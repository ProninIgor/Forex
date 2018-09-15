using System;
using System.Collections.Generic;
using Common.Data;
using Common.Entities;

namespace Common.Interfaces
{
    /// <summary>
    /// Контракт обработанных данных с биржи
    /// </summary>
    public interface IStockExcangeObjectManager
    {
        string Name { get; } 
        List<TickDTO> GetTicks(int marketId, PeriodType periodType);
        List<TickDTO> GetLastTicks(int marketId, PeriodType periodType, TimeSpan offset);
        List<CurrencyDTO> GetCurrencies();
        List<MarketDTO> GetMarkets();

        /// <summary>
        /// Получить открытые ставки на покупку и продажу (все ставки)
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        List<OrderDTO> GetOrders(int marketId);

        List<MarketSummaryDTO> GetMarketSummaries();

        MarketSummaryDTO GetMarketSummary(int marketId);

        List<OpenOrderDTO> GetOpenOrders(int marketId);
    }
}