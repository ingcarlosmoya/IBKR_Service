using IBKR_Service.Config;
using IBKR_Service.Services;
using IBKR_TradeBridge;
using IBKR_TradeBridge.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IBKR_Service.Handlers
{
    public class ConfirmationResponseHanlder : ResponseHandler
    {

        public ConfirmationResponseHanlder(ILogger<Worker> logger, ApiMessenger messenger, IOptions<BridgeSettings> bridgeSettings) : base(logger, messenger, bridgeSettings)
        {
        }

        public override async Task Handle(string jsonResponse, ResponseHandler? middleWorkHandler)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<List<ConfirmationResponse>>(jsonResponse);
                if (response != null && response.Any())
                {
                    var confirmation = response.FirstOrDefault();
                    if (confirmation != null && confirmation.message != null && confirmation.message.Any())
                    {
                        _logger.LogInformation(string.Join(",", confirmation.message));
                        if (!string.IsNullOrEmpty(confirmation.id))
                        {
                            var replyRequest = new ReplyRequest { confirmed = true };
                            var jsonRequest = JsonConvert.SerializeObject(replyRequest);
                            var replyResponse = await _apiMessenger.PostAsyncJsonResponse($"{_bridgeSettings}/iserver/reply/{confirmation.id}", jsonRequest);
                            if (middleWorkHandler != null)
                                await middleWorkHandler.Handle(replyResponse);
                        }
                    }
                }

            }
            catch (Exception)
            {
                if (_next != null)
                    await _next.Handle(jsonResponse);
            }
        }
    }
}

