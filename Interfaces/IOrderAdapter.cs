using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Interfaces;
public interface IOrderAdapter
{
    Task<bool> ApplyMapping(Mt5TradeTransaction mt5Order);
}
