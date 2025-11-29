using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Handlers;
public class SymbolValidationHandler : IOrderHandler
{
    public Task Handle(TradeContext tradeContext)
    {
		try
		{
            tradeContext.IsStepSuccesful = Task.FromResult(tradeContext != null && !string.IsNullOrWhiteSpace(tradeContext.Mt5Order.Symbol)); 
            return Task.CompletedTask;
        }
		catch (Exception)
		{
			throw;
		}
    }
}
