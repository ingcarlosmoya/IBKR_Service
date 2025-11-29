using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Models;
namespace IBKR_TradeBridge.Handlers;
public class RiskCheckHandler : IOrderHandler
{
    public Task Handle(TradeContext tradeContext)
    {
        // minimal risk check (demo)
        try
        {
            if (tradeContext != null && tradeContext.Mt5Order != null)
            {
                var mt5Order = tradeContext.Mt5Order;
                if (mt5Order.Volume <= 0) return Task.FromResult(false);
                if (mt5Order.Volume < 1)
                    mt5Order.Volume = 1;

                tradeContext.IsStepSuccesful = Task.FromResult(true);
            }

            return Task.CompletedTask;
        }
        catch (Exception)
        {

            throw;
        }
        
    }
}
