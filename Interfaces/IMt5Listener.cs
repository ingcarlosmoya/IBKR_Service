using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Interfaces;
public interface IMt5Listener
{
    Task StartAsync(CancellationToken cancellationToken);
}
