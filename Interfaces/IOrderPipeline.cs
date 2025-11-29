using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Interfaces;
public interface IOrderPipeline
{
    bool IsOrderExecuted { get; }
    Task ExecuteAsync(Mt5TradeTransaction evt);
}

