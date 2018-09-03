using System;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using Common.Entities;
using Enums;

namespace Common
{
    public partial class OrderManager
    {
        private double GetCurrentRate(int marketId)
        {
            return this.ObjectManager.GetCurrentMarketValue(marketId);
        }

        /// <summary>
        /// Привязаться к анализу оредеров на покупку, чтобы загнать цену как можно ниже
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        private double GetBuyRate(int marketId)
        {

            return GetCurrentRate(marketId);
        }

        private double GetQuantity(int marketId)
        {
            //todo определение размера ставки исходя из минимального значения, успешности метода вычисления и вероятности попадания. подтверждения со стороны пользователя
            return 0.0005;
        }

        private OrderCandidate GetBuyOrderCandidate(int marketId)
        {
            StakeSectionBuy stakeSectionBuy = this.AnalizeManager.Calculate(marketId, AlgorithmCalculateType.Agvtype);
            if (stakeSectionBuy == null)
                return null;

            double currentRate = GetCurrentRate(marketId);

            //todo проверки на маркетИд
            if (stakeSectionBuy.InSection(currentRate))
            {
                double buyRate = GetBuyRate(marketId);
                //todo проверку на вхождение в диапазон

                double quantity = GetQuantity(marketId);
                return new OrderCandidate(){Id = Guid.NewGuid(), MarketId = marketId, OrderType = OrderType.Buy, Rate = buyRate, Quantity = quantity};
            }

            return null;

        }

        public void Working()
        {
            while (true)
            {
                Thread.Sleep(1*1000);
                foreach (int marketId in marketIds)
                {
                    if (HasBuyOrder(marketId))
                    {
                        //todo описать процесс поиска ставки на продажу
                        continue;
                    }


                    OrderCandidate orderCandidate = GetBuyOrderCandidate(marketId);

                    if (orderCandidate == null)
                    {
                       continue;
                    }

                    this.orderCandidates.TryUpdate(GetBuyKey(marketId), orderCandidate, null);
                }
            }
        }
    }
}