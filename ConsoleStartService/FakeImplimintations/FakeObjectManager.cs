using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Data;
using Common.Entities;
using Common.Interfaces;

namespace ConsoleStartService.FakeImplimintations
{
    public class FakeObjectManager : IStockExcangeObjectManager
    {
        public string Name { get; }

        private int index = 1;

        private int k = 1;

        private bool isStart;
            
        public List<TickDTO> GetTicks(int marketId, PeriodType periodType)
        {
            throw new NotImplementedException();
        }

        public List<TickDTO> GetLastTicks(int marketId, PeriodType periodType, TimeSpan offset)
        {
            if (!isStart)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(1 * 200);
                        
                        if (index > 59)
                        {
                            index = 0;
                        }

                        index++;
                    }
                });
            }

//            var currentIndex = index;
            int second = index;

            decimal fValue = GetValue(second);
            
            List<TickDTO> res = new List<TickDTO>();
            List<double> values = new List<double>();
            double a = 0.001;
            double b = 0.001;
            double c = 0.5;
            double d = 0;
            
            
            for (int x = second; x > second - 30; x--)
            {
                int v = x;
                if (x < 0)
                {
                    v = 60 + x;
                }

//                decimal min = 0;
//                var i = index - x;
//                if (i > 20)
//                {
//                    min = 20-i;
//                }
//                else
//                {
//                    min = i;
//                }
                decimal fv = GetValue(v);
                decimal fv1 = GetValue(v + 1);
                res.Add(new TickDTO(){HighValue = Math.Max(fv, fv1), LowValue = Math.Min(fv, fv1)});
            }

            return res;
        }

        private decimal GetValue(int second)
        {
            int Oy = 5;
            if (second <= 30)
            {
                return second + Oy;
            }
            else
            {
                return 60 + Oy - second;
            }
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
            return new MarketSummaryDTO(){Last = GetValue(index)};
        }

        public List<OpenOrderDTO> GetOpenOrders(int marketId)
        {
            throw new NotImplementedException();
        }
    }
}