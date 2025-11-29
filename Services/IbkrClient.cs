using IBKR_Service.Config;
using IBKR_Service.Handlers;
using IBKR_Service.Interfaces;
using IBKR_Service.Services;
using IBKR_TradeBridge.Config;
using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;
namespace IBKR_TradeBridge.Services;
public class IbkrClient : IIbkrClient
{
    public bool IsConnected { get; private set; }

    private readonly BridgeSettings _bridgeSettings;
    private readonly IApiMessenger _apiMessenger;
    private readonly ILogger _logger;
    private SuccessfulResponseHanlder _successfulResponseHandler;
    private ConfirmationResponseHanlder _confirmationResponseHandler;
    private ErrorResponseHanlder _errorResponseHandler;
    private CheckPnlResponseHanlder _checkPnlResponseHanlder;


    public IbkrClient(ILogger<Worker> logger, ApiMessenger apiMessenger, IOptions<BridgeSettings> bridgeSettings,
        SuccessfulResponseHanlder successfulResponseHanlder, ConfirmationResponseHanlder confirmationResponseHanlder, ErrorResponseHanlder errorResponseHanlder,
        CheckPnlResponseHanlder checkPnlResponseHanlder)
    {
        _logger = logger;
        _bridgeSettings = bridgeSettings.Value;
        _apiMessenger = apiMessenger;
        _successfulResponseHandler = successfulResponseHanlder;
        _confirmationResponseHandler = confirmationResponseHanlder;
        _errorResponseHandler = errorResponseHanlder;
        _checkPnlResponseHanlder = checkPnlResponseHanlder;
    }
    public async Task SendOrderAsync(IbkrOrder order)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new LowerCaseNamingPolicy(),
            };

            var orders = new IbkrOrder();
            orders.Orders = new List<IbkrOrder>();
            orders.Orders.Add(order);
            var mt5OrderJson = JsonSerializer.Serialize(orders, options);
            var response = await _apiMessenger.PostAsync($"{_bridgeSettings.GatewayUrl}/iserver/account/{_bridgeSettings.AccountId}/orders", mt5OrderJson);
            IsConnected = response.IsSuccessStatusCode;
            if (!IsConnected)
                LogError("Send order", response);

            var responseJson = await response.Content.ReadAsStringAsync();
            _successfulResponseHandler.SetNext(_confirmationResponseHandler);
            _confirmationResponseHandler.SetNext(_errorResponseHandler);
            _logger.LogWarning($"[IbkrClient] Sending {order.Action} {order.Quantity} {order.Symbol}");
            await _successfulResponseHandler.Handle(responseJson);

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task CheckPnl()
    {
        try
        {
            var response = await _apiMessenger.GetAsync($"{_bridgeSettings.GatewayUrl}{_bridgeSettings.PnlEndpoint}");
            IsConnected = response.IsSuccessStatusCode;
            if (!IsConnected)
                LogError("Check PNL", response);

            var responseJson = await response.Content.ReadAsStringAsync();
            _checkPnlResponseHanlder.SetNext(_errorResponseHandler);
            await _checkPnlResponseHanlder.Handle(responseJson);

        }
        catch (Exception)
        {
            throw;
        }
        
    }

    public async Task Tickle()
    {
        try
        {
            var response = await _apiMessenger.GetAsync($"{_bridgeSettings.GatewayUrl}{_bridgeSettings.TickleEndpoint}");
            IsConnected = response.IsSuccessStatusCode;
            if (!IsConnected)
                LogError("Tickle", response);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void LogError(string callerName, HttpResponseMessage httpResponseMessage) {
        _logger.LogError($"{callerName} process has failed due to Code: {(int)httpResponseMessage.StatusCode}, Reason: {httpResponseMessage.ReasonPhrase}");
    }
}

public class LowerCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
        => name.ToLower();
}
