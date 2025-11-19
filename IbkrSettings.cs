using System;
using System.Threading.Tasks;

namespace IBKR_Service.Config
{
    public class IbkrSettings
    {
        public string GatewayUrl { get; set; } = string.Empty;
        public string SessionCookie { get; set; } = string.Empty;
        public string AccountId { get; set; } = string.Empty;
        public string TickleEndpoint { get; set; } = string.Empty;
        public string PnlEndpoint { get; set; } = string.Empty;
        
    }
}
