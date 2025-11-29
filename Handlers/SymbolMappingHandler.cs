using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Handlers;
public class SymbolMappingHandler : IOrderHandler
{
    private readonly IOrderAdapter _adapter;
    public SymbolMappingHandler(IOrderAdapter adapter) { _adapter = adapter; }
    public Task Handle(TradeContext tradeContext)
    {
        try
        {
            if (tradeContext != null && tradeContext.Mt5Order != null)
            {
                tradeContext.IsStepSuccesful = _adapter.ApplyMapping(tradeContext.Mt5Order);
            }
            return Task.CompletedTask;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
