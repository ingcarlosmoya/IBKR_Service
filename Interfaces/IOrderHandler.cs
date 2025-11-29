using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Interfaces;
public interface IOrderHandler
{
    Task Handle(TradeContext tradeContext);
}
