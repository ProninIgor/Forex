using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Data;
using Common.Interfaces;
using Common.IoC;
using Ninject.Parameters;

namespace Bittrex.Core
{
    
    public class MonitoringManager : IMonitoringManager
    {
        private Dictionary<int, MarketOrderStatus> _marketOrderStatuses = new Dictionary<int, MarketOrderStatus>();
        private int[] marketIds;
        public IOrderManager OrderManager { get; }

        public ITradeManager TradeManager { get; }

        public MonitoringManager(int[] marketIds, IStockExcangeObjectManager stockExcangeObjectManager, IObjectManager objectManager)
        {
            if (marketIds.Distinct().Count() != marketIds.Length)
                throw new ArgumentException("Only unique marketIds"); 

            this.marketIds = marketIds;


            List<IParameter> parameters = new List<IParameter>();
            parameters.Add(IoC.GetCtorParam("marketId", marketIds));
            parameters.Add(IoC.GetCtorParam("stockExcangeObjectManager", stockExcangeObjectManager));
            parameters.Add(IoC.GetCtorParam("objectManager", objectManager));
            
            OrderManager = IoC.Get<IOrderManager>(parameters);
            TradeManager = IoC.Get<ITradeManager>();
        }

        public void Init()
        {
            //Проставляем статусы для маркетов за которыми будем следить
            foreach (int marketId in this.marketIds)
            {
                _marketOrderStatuses.Add(marketId, new MarketOrderStatus());
            }

            // стартуем модуль ставо для конкретных маркетов
            Task.Run(() => OrderManager.Working());
        }

        /// <summary>
        /// Логика усановки ставок на снятие и установку
        /// </summary>
        public void Start()
        {
            for (var index = 0;;)
            {
                if (index > marketIds.Length -1)
                {
                    index = 0;
                }

                int marketId = marketIds[index];
                MarketOrderStatus marketOrderStatus = _marketOrderStatuses[marketId];

                if (!marketOrderStatus.IsActive)
                {
                    index++;
                    continue;
                }

                if (!marketOrderStatus.IsFindBuyOrder && this.OrderManager.HasBuyOrder(marketId))
                {
                    marketOrderStatus.IsFindBuyOrder = true;
                    continue;
                }

                if (marketOrderStatus.IsFindBuyOrder)
                {
                    OrderDTO buyOrderDto = this.OrderManager.GetBuyOrder(marketId);

                    if (buyOrderDto != null)
                    {
                        bool isOrderComplited = this.TradeManager.SetBuyOrder(marketId, buyOrderDto.Quantity, buyOrderDto.Rate);
                        if (isOrderComplited)
                        {
                            marketOrderStatus.IsHaveOneBuyOrder = true;
                            OrderManager.BuyOrderComplited(marketId);
                        }
                    }

                    marketOrderStatus.IsFindBuyOrder = false;
                    index++;
                }

                if (!marketOrderStatus.IsFindSellOrder && this.OrderManager.HasSellOrder(marketId))
                {
                    marketOrderStatus.IsFindSellOrder = true;
                    continue;
                }

                if (marketOrderStatus.IsFindSellOrder)
                {
                    OrderDTO sellOrderDto = this.OrderManager.GetSellOrder(marketId);

                    if (sellOrderDto != null)
                    {
                        bool isOrderComplited = this.TradeManager.SetSellOrder(marketId, sellOrderDto.Quantity, sellOrderDto.Rate);
                        if (isOrderComplited)
                        {
                            marketOrderStatus.IsHaveOneSellOrder = true;
                            OrderManager.SellOrderComplited(marketId);
                        }
                    }

                    marketOrderStatus.IsFindSellOrder = false;
                    index++;
                }

                index++;
            }
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Abort()
        {
            throw new System.NotImplementedException();
        }
    }
}