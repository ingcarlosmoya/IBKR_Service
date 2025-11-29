using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Interfaces;
public interface IIbkrClient
{
    Task SendOrderAsync(IbkrOrder order);
    bool IsConnected { get; }
    Task CheckPnl();
    Task Tickle();
}
