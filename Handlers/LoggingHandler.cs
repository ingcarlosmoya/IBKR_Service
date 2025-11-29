using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Handlers;
public class LoggingHandler : IOrderHandler
{
    Task IOrderHandler.Handle(TradeContext tradeContext)
    {
        return Task.CompletedTask;
    }
}
