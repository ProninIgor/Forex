using System;
using System.Collections.Generic;
using Common.Data;
using Common.Entities;
using Common.Interfaces;

namespace ConsoleStartService.FakeImplimintations
{
    public class FakeObjectManager : IStockExcangeObjectManager
    {
        public string Name { get; }
        public List<TickDTO> GetTicks(int marketId, PeriodType periodType)
        {
            throw new NotImplementedException();
        }

        public List<TickDTO> GetLastTicks(int marketId, PeriodType periodType, TimeSpan offset)
        {
            return new List<TickDTO>()
            {
                new TickDTO() {HighValue = 0.00095m, LowValue = 0.00092m},
                new TickDTO() {HighValue = 0.00100m, LowValue = 0.00095m},
                new TickDTO() {HighValue = 0.00092m, LowValue = 0.00092m},
                new TickDTO() {HighValue = 0.00092m, LowValue = 0.00090m},
                new TickDTO() {HighValue = 0.00092m, LowValue = 0.00097m},
                new TickDTO() {HighValue = 0.00097m, LowValue = 0.00092m}
            };
        }

        public List<CurrencyDTO> GetCurrencies()
        {
            throw new NotImplementedException();
        }

        public List<MarketDTO> GetMarkets()
        {
            throw new NotImplementedException();
        }

        public List<OrderDTO> GetOrders(int marketId)
        {
            throw new NotImplementedException();
        }

        public List<MarketSummaryDTO> GetMarketSummaries()
        {
            throw new NotImplementedException();
        }

        public MarketSummaryDTO GetMarketSummary(int marketId)
        {
            return new MarketSummaryDTO(){Last = 0.000905m};
        }

        public List<OpenOrderDTO> GetOpenOrders(int marketId)
        {
            throw new NotImplementedException();
        }
    }
}