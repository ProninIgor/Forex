using System;
using System.Collections.Generic;
using Common.Data;

namespace Common
{
    public interface IStockExcangeObjectManager
    {
        string Name { get; } 
        List<Tick> GetTicks(string market, string periodType);
        List<Tick> GetLastTicks(string market, string periodType, TimeSpan offset);
        List<Currency> GetCurrencies();
        List<Market> GetMarkets();

        List<Order> GetOrders(string marketName);

        List<MarketSummary> GetMarketSummaries();

        MarketSummary GetMarketSummary(string marketName);

        List<OpenOrder> GetOpenOrders(string apiKey, string marketName);
    }
}