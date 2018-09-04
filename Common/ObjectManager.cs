using System;
using System.Collections.Generic;
using Common.Data;
using Common.Entities;
using Common.Interfaces;

namespace Common
{
    /// <summary>
    /// Менеджер обработанных данных
    /// </summary>
    public class ObjectManager : IObjectManager
    {
        /// <summary>
        /// Обработанные данные с биржи
        /// </summary>
        public IStockExcangeObjectManager StockExcangeObjectManager { get; set; }
        
        public ObjectManager(IStockExcangeObjectManager stockExcangeObjectManager)
        {
            StockExcangeObjectManager = stockExcangeObjectManager;
        }

        public double GetCurrentMarketValue(int marketId)
        {
            MarketSummaryDTO marketSummaryDto = this.StockExcangeObjectManager.GetMarketSummary(marketId);
            return marketSummaryDto.Last;
        }

        public double GetCurrentMarketBuyValue(int marketId)
        {
            throw new System.NotImplementedException();
        }

        public double GetCurrentMarketSellValue(int marketId)
        {
            throw new System.NotImplementedException();
        }

        public List<TickDTO> GetTicks(int marketId, string periodType)
        {
            throw new NotImplementedException();
        }

        public List<TickDTO> GetLastTicks(int marketId, PeriodType periodType, TimeSpan offset)
        {
            List<TickDTO> lastTicks = this.StockExcangeObjectManager.GetLastTicks(marketId, periodType, offset);
            return lastTicks;
        }
    }
}