using System;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using Common.CalculateClasses;
using Common.Entities;
using Common.Interfaces;
using Enums;

namespace Common
{
    /// <summary>
    /// Менеджер ставок. Хранит в себе инфу по ставкам, принимает решения о том, как расчитывать ставки
    /// </summary>
    public partial class OrderManager
    {
        private decimal GetCurrentRate(int marketId)
        {
            return this.ObjectManager.GetCurrentMarketValue(marketId);
        }

        /// <summary>
        /// Привязаться к анализу оредеров на покупку, чтобы загнать цену как можно ниже
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        private decimal GetBuyRate(int marketId)
        {

            return GetCurrentRate(marketId);
        }

        private decimal GetQuantity(int marketId)
        {
            //todo определение размера ставки исходя из минимального значения, успешности метода вычисления и вероятности попадания. подтверждения со стороны пользователя
            return 0.0005m;
        }

        private OrderCandidate GetBuyOrderCandidate(int marketId)
        {
            ICalculateClass avgCalculateClass = new AvgCalculateClass(this.ObjectManager);
            
            StakeSectionBuy stakeSectionBuy = avgCalculateClass.Calculate(marketId);
            if (stakeSectionBuy == null)
                return null;

            decimal currentRate = GetCurrentRate(marketId);

            //todo проверки на маркетИд
            if (stakeSectionBuy.InSection(currentRate))
            {
                decimal buyRate = GetBuyRate(marketId);
                //todo проверку на вхождение в диапазон

                decimal quantity = GetQuantity(marketId);
                return new OrderCandidate(){Id = Guid.NewGuid(), MarketId = marketId, OrderType = OrderType.Buy, Rate = buyRate, Quantity = quantity};
            }

            return null;

        }

        /// <summary>
        /// Запускает работу модуля
        /// </summary>
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