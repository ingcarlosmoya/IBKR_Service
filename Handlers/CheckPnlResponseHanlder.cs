using IBKR_Service.Config;
using IBKR_Service.Interfaces;
using IBKR_Service.Services;
using IBKR_TradeBridge;
using IBKR_TradeBridge.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace IBKR_Service.Handlers
{
    public class CheckPnlResponseHanlder : ResponseHandler
    {
        public CheckPnlResponseHanlder(ILogger<Worker> logger, ApiMessenger messenger, IOptions<BridgeSettings> bridgeSettings) : base(logger, messenger, bridgeSettings)
        {
        }

        public override Task Handle(string jsonResponse, ResponseHandler? middleWorkHandler = null)
        {
            try
            {
                var response = JObject.Parse(jsonResponse);
                string dpl = "$0";
                if (response["upnl"] != null && response["upnl"]["DUN296642.Core"] != null && response["upnl"]["DUN296642.Core"]["dpl"] != null)
                {
                    dpl = (string)response["upnl"]["DUN296642.Core"]["dpl"];
                    _logger.LogInformation($">>> Pnl: {dpl}");
                }

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

