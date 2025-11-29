using IBKR_TradeBridge.Interfaces;
using IBKR_TradeBridge.Models;
using System.Runtime.InteropServices;
namespace IBKR_TradeBridge.Adapters;
public class CfdToFutureAdapter : IOrderAdapter
{
    private readonly Dictionary<string, string> _map = new()
    {
        {"NAS100","MNQ"},
        {"XAUUSD","MGC"},
        {"US30","MYM"},
        {"ETHUSD","MYM"}

    };

    private static Dictionary<string, int> _contractId = new Dictionary<string, int>
    {
        { "MNQ", 730283094 },
        { "MYM", 362688015 },
        { "MGC", 79702479 }
     };

    public Task<bool> ApplyMapping(Mt5TradeTransaction mt5Order)
    {
        var key = mt5Order.Symbol.ToUpperInvariant();
        string? symbol;
        int contractId = 0;
        if (_map.TryGetValue(key, out symbol) && _contractId.TryGetValue(symbol, out contractId))
        {
            mt5Order.Symbol = symbol;
            mt5Order.ConId = contractId;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
