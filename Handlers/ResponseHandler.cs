using IBKR_Service.Config;
using IBKR_Service.Interfaces;
using IBKR_Service.Services;
using IBKR_TradeBridge;
using IBKR_TradeBridge.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace IBKR_Service.Handlers
{


    public abstract class ResponseHandler :IResponseHandler
    {
        protected IResponseHandler? _next;
        protected ILogger<Worker> _logger;
        protected IApiMessenger _apiMessenger;
        protected BridgeSettings _bridgeSettings;

        public abstract Task Handle(string jsonResponse, ResponseHandler? middleWorkHandler = null);

        public void SetNext(IResponseHandler handler)
        {
            _next = handler;
        }

        public ResponseHandler(ILogger<Worker> logger, ApiMessenger messenger, IOptions<BridgeSettings> bridgeSettings) { 
            _logger = logger;
            _apiMessenger = messenger;
            _bridgeSettings= bridgeSettings.Value;
        }
    }
}

