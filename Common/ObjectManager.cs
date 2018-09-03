using System;
using System.Collections.Generic;
using Common.Data;
using Common.Entities;

namespace Common
{
    public class ObjectManager : IObjectManager
    {
        public IStockExcangeObjectManager StockExcangeObjectManager { get; set; }
        public Dictionary<int, string> MarketNameById { get; }


        public ObjectManager(IStockExcangeObjectManager stockExcangeObjectManager, Dictionary<int, string> marketNameById)
        {
            StockExcangeObjectManager = stockExcangeObjectManager;
            MarketNameById = marketNameById;
        }

        public double GetCurrentMarketValue(int marketId)
        {
            string marketName = this.MarketNameById[marketId];
            MarketSummaryDTO marketSummaryDto = this.StockExcangeObjectManager.GetMarketSummary(marketName);
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
            string marketName = this.MarketNameById[marketId];
            List<TickDTO> lastTicks = this.StockExcangeObjectManager.GetLastTicks(marketName, periodType, offset);
            return lastTicks;
        }
    }
}