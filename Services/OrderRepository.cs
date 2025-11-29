using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Services;
public class OrderRepository : IOrderRepository
{
    // Simple in-memory store for demo
    private readonly List<IbkrOrder> _store = new();
    public Task SaveAsync(IbkrOrder order)
    {
        _store.Add(order);
        return Task.CompletedTask;
    }
}
