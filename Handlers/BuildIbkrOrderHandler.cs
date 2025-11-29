using IBKR_Service;
using IBKR_Service.Config;
using IBKR_TradeBridge.Config;
using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Models;
using Microsoft.Extensions.Options;
namespace IBKR_TradeBridge.Handlers;
public class BuildIbkrOrderHandler : IOrderHandler
{
    private readonly BridgeSettings _bridgeSettings;
    public BuildIbkrOrderHandler(IOptions<BridgeSettings> bridgeSettings)
    {
        _bridgeSettings = bridgeSettings.Value;
    }
    public Task Handle(TradeContext tradeContext)
    {
        try
        {
            if (tradeContext != null && tradeContext.Mt5Order != null)
            {
                var mt5Order = tradeContext.Mt5Order;
                tradeContext.IbkrOrder = new IbkrOrder()
                {
                    AcctId = _bridgeSettings.AccountId,
                    Conid = mt5Order.ConId,
                    OrderType = "MKT",
                    Quantity = mt5Order.Volume,
                    Side = mt5Order.Action,
                    Ticker = mt5Order.Symbol,
                    Tif = "GTC"
                };
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
