using System;
using System.Collections.Generic;
using Common.Data;
using Common.Entities;

namespace Common
{
    public interface IObjectManager
    {
        double GetCurrentMarketValue(int marketId);
        double GetCurrentMarketBuyValue(int marketId);
        double GetCurrentMarketSellValue(int marketId);
        List<Tick> GetTicks(int marketId, string periodType);

        /// <summary>
        /// Получить тики за последний период
        /// </summary>
        /// <param name="marketId">ИД магазина</param>
        /// <param name="periodType">Период агрегации (минута, 15 минут, 30 минут)</param>
        /// <param name="offset">Временной промежуток</param>
        /// <returns></returns>
        List<Tick> GetLastTicks(int marketId, string periodType, TimeSpan offset);
    }

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
            MarketSummary marketSummary = this.StockExcangeObjectManager.GetMarketSummary(marketName);
            return marketSummary.Last;
        }

        public double GetCurrentMarketBuyValue(int marketId)
        {
            throw new System.NotImplementedException();
        }

        public double GetCurrentMarketSellValue(int marketId)
        {
            throw new System.NotImplementedException();
        }

        public List<Tick> GetTicks(int marketId, string periodType)
        {
            throw new NotImplementedException();
        }

        public List<Tick> GetLastTicks(int marketId, string periodType, TimeSpan offset)
        {
            string marketName = this.MarketNameById[marketId];
            List<Tick> lastTicks = this.StockExcangeObjectManager.GetLastTicks(marketName, periodType, offset);
            return lastTicks;
        }
    }
}