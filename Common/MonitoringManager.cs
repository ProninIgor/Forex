using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            parameters.Add(IoC.GetCtorParam("marketIds", marketIds));
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
                Thread.Sleep(100);
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
                
                // если уже есть ставка на покупку или продажу, то делать ничего не надо
                if (this.OrderManager.HasBuyOrder(marketId)) 
                {
                    // определение исполненного ордера на покупку и на продажу.
                    this.OrderManager.BuyOrderCompleted(marketId);
                    continue;
                }

                if (this.OrderManager.HasSellOrder(marketId))
                {
                    // определение исполненного ордера на покупку и на продажу.
                    this.OrderManager.SellOrderCompleted(marketId);
                    continue;
                }

                if (OrderManager.IsFindOrderBuy(marketId))
                {
                    OrderDTO buyOrderDto = this.OrderManager.GetBuyOrder(marketId);
                    if (buyOrderDto != null)
                    {
                        bool isOrderCompleted = this.TradeManager.SetBuyOrder(marketId, buyOrderDto.Quantity, buyOrderDto.Rate);
                        if(isOrderCompleted)
                        {
                            OrderManager.SetOrderBuy(marketId);
                        }
                    }

                }
                
                if (OrderManager.IsFindOrderSell(marketId))
                {
                    OrderDTO sellOrder = this.OrderManager.GetSellOrder(marketId);
                    if (sellOrder != null)
                    {
                        bool isOrderCompleted = this.TradeManager.SetSellOrder(marketId, sellOrder.Quantity, sellOrder.Rate);
                        if(isOrderCompleted)
                        {
                            OrderManager.SetOrderSell(marketId);
                        }
                    }

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