using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;

namespace Bittrex.Core
{
    /// <summary>
    /// Главный менеджер для работы со ставками
    /// </summary>
    public interface IMonitoringManager
    {
        IOrderManager OrderManager { get; }

        ITradeManager TradeManager { get;}

        void Init();
        void Start();
        void Stop();
        void Abort();
    }


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
        }

        public void Init()
        {
            foreach (int marketId in this.marketIds)
            {
                _marketOrderStatuses.Add(marketId, new MarketOrderStatus());
            }
        }

        public void Start()
        {
            for (var index = 0;;)
            {
                if (index < marketIds.Length)
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

    internal class MarketOrderStatus
    {
        public MarketOrderStatus()
        {
            this.IsActive = true;
        }
        public bool IsActive { get; set; }
        public bool IsFindSellOrder { get; set; }
        public bool IsFindBuyOrder { get; set; }
        public bool IsHaveOneBuyOrder { get; set; }
        public bool IsHaveOneSellOrder { get; set; }
        
    }
}