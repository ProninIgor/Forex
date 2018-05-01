using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Data;

namespace Bittrex.Core
{
    public class MonitoringManager : IMonitoringManager
    {
        private Dictionary<int, MarketOrderStatus> _marketOrderStatuses = new Dictionary<int, MarketOrderStatus>();
        private int[] marketIds;
        public IOrderManager OrderManager { get; }

        public ITradeManager TradeManager { get; }

        public MonitoringManager(int[] marketIds)
        {
            if (marketIds.Distinct().Count() != marketIds.Length)
                throw new ArgumentException("Only unique marketIds"); 

            this.marketIds = marketIds;

            OrderManager = new OrderManager(marketIds);
            TradeManager = new TradeManager();
        }

        public void Init()
        {
            foreach (int marketId in this.marketIds)
            {
                _marketOrderStatuses.Add(marketId, new MarketOrderStatus());
            }

            Task.Run(() => OrderManager.Working());
        }

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
                    Order buyOrder = this.OrderManager.GetBuyOrder(marketId);

                    if (buyOrder != null)
                    {
                        bool isOrderComplited = this.TradeManager.SetBuyOrder(marketId, buyOrder.Quantity, buyOrder.Rate);
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
                    Order sellOrder = this.OrderManager.GetSellOrder(marketId);

                    if (sellOrder != null)
                    {
                        bool isOrderComplited = this.TradeManager.SetSellOrder(marketId, sellOrder.Quantity, sellOrder.Rate);
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