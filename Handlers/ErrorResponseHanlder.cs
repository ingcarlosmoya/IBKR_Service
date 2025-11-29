using IBKR_Service.Config;
using IBKR_Service.Services;
using IBKR_TradeBridge;
using IBKR_TradeBridge.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IBKR_Service.Handlers
{
    public class ErrorResponseHanlder : ResponseHandler
    {
        public ErrorResponseHanlder(ILogger<Worker> logger, ApiMessenger messenger, IOptions<BridgeSettings> bridgeSettings) : base(logger, messenger, bridgeSettings)
        {
        }

        public override Task Handle(string jsonResponse, ResponseHandler? middleWorkHandler = null)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);
                if (response != null)
                    _logger.LogError($"Order not created, error: {response.error}");

            }
            catch (Exception)
            {
                if (_next != null)
                    _next.Handle(jsonResponse);
            }
            return Task.CompletedTask;
        }
    }
}

