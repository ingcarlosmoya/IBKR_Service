using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Models;
using IBKR_TradeBridge.Services;
namespace IBKR_TradeBridge.Handlers;
public class SendToIbkrHandler : IOrderHandler
{
    private readonly IIbkrClient _ibkrClient;
    private readonly IOrderRepository _repo;
    public SendToIbkrHandler(IIbkrClient ibkrClient, IOrderRepository repo)
    {
        _ibkrClient = ibkrClient; _repo = repo;
    }

    Task IOrderHandler.Handle(TradeContext tradeContext)
    {
        try
        {
            if (tradeContext != null && tradeContext.IbkrOrder != null) {
                _ibkrClient.SendOrderAsync(tradeContext.IbkrOrder);
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