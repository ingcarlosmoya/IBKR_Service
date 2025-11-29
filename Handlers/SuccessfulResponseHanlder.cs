using IBKR_Service.Config;
using IBKR_Service.Interfaces;
using IBKR_Service.Services;
using IBKR_TradeBridge;
using IBKR_TradeBridge.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IBKR_Service.Handlers
{
    public class SuccessfulResponseHanlder : ResponseHandler, IResponseHandler
    {

        public SuccessfulResponseHanlder(ILogger<Worker> logger, ApiMessenger messenger, IOptions<BridgeSettings> bridgeSettings) : base(logger, messenger, bridgeSettings)
        {
        }

        public override Task Handle(string jsonResponse, ResponseHandler? middleWorkHandler = null)
        {
            try
            {
                var succesfulResponseList = JsonConvert.DeserializeObject<List<SuccessfulResponse>>(jsonResponse);
                if (succesfulResponseList != null && succesfulResponseList.Any())
                {
                    var succesfulResponse = succesfulResponseList.FirstOrDefault();
                    if (succesfulResponse != null && string.IsNullOrEmpty(succesfulResponse.order_id))
                        _logger.LogInformation($"Order created, ID: {succesfulResponse.order_id}");
                }
            }
            catch (Exception)
            {
                if (_next != null)
                    _next.Handle(jsonResponse, this);
            }
            return Task.CompletedTask;
        }
    }
}

