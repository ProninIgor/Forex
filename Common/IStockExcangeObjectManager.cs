using System;
using System.Collections.Generic;
using Common.Data;
using Common.Entities;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStockExcangeObjectManager
    {
        string Name { get; } 
        List<TickDTO> GetTicks(string market, PeriodType periodType);
        List<TickDTO> GetLastTicks(string market, PeriodType periodType, TimeSpan offset);
        List<CurrencyDTO> GetCurrencies();
        List<MarketDTO> GetMarkets();

        List<OrderDTO> GetOrders(string marketName);

        List<MarketSummaryDTO> GetMarketSummaries();

        MarketSummaryDTO GetMarketSummary(string marketName);

        List<OpenOrder> GetOpenOrders(string apiKey, string marketName);
    }
}