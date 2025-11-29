namespace IBKR_TradeBridge.Config;
public class BridgeSettings
{
    public int Mt5Port { get; set; }
    public string IbkrHost { get; set; } = default!;
    public int IbkrPort { get; set; }
    public int ClientId { get; set; }
    public string GatewayUrl { get; set; } = string.Empty;
    public string SessionCookie { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public string TickleEndpoint { get; set; } = string.Empty;
    public string PnlEndpoint { get; set; } = string.Empty;
    public string WorkingDirectory { get; set; } = string.Empty;
    public string CommandArguments { get; set; } = string.Empty;
    public string OrdersFilePath { get; set; } = string.Empty;
}
