using System;
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
            //Todo переделать на реальное получение
            return 0.005;
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
            AnalizeSection analizeSection = this.AnalizeManager.Calculate(marketId);
            if (analizeSection == null)
                return null;

            double currentRate = GetCurrentRate(marketId);

            //todo проверки на маркетИд
            if (analizeSection.InSection(currentRate) && marketId == 2)
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