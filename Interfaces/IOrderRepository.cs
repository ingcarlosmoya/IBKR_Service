using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Interfaces;
public interface IOrderRepository
{
    Task SaveAsync(IbkrOrder order);
}
