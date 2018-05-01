using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Bittrex.Core;
using Common.Data;
using Enums;

namespace Common
{
    public partial class OrderManager : IOrderManager
    {
        //todo: Сделать возможным только один кандижат на маркет и тип. Или нотифицировать по Guid для идентификации кандидата
        private ConcurrentDictionary<string, OrderCandidate> orderCandidates = new ConcurrentDictionary<string, OrderCandidate>();
        private int[] marketIds;
        private IAnalizeManager AnalizeManager { get; set; }
 

        public OrderManager(int[] marketIds)
        {
            this.marketIds = marketIds;
            foreach (int marketId in marketIds)
            {
                string key = GetBuyKey(marketId);
                orderCandidates.TryAdd(key, null);
                key = GetSellKey(marketId);
                orderCandidates.TryAdd(key, null);
            }
            this.AnalizeManager = new AnalizeManager();
        }

        private string GetSellKey(int marketId)
        {
            return GetKey(marketId, OrderType.Sell);
        }

        private string GetBuyKey(int marketId)
        {
            return GetKey(marketId, OrderType.Buy);
        }

        public bool HasBuyOrder(int marketId)
        {
            return this.orderCandidates[GetBuyKey(marketId)] != null;
        }

        public bool HasSellOrder(int marketId)
        {
            return this.orderCandidates[GetSellKey(marketId)] != null;
        }

        public Order GetBuyOrder(int marketId)
        {
            OrderCandidate orderCandidate = this.orderCandidates[GetBuyKey(marketId)];

            Order buyOrder = orderCandidate == null 
                ? null 
                : new Order(){OrderType = OrderType.Buy, Quantity = orderCandidate.Quantity, Rate = orderCandidate.Rate};

            return buyOrder;
        }

        public Order GetSellOrder(int marketId)
        {
            OrderCandidate orderCandidate = this.orderCandidates[GetSellKey(marketId)];

            Order sellOrder = orderCandidate == null
                ? null
                : new Order() { OrderType = OrderType.Sell, Quantity = orderCandidate.Quantity, Rate = orderCandidate.Rate };

            return sellOrder;
        }

        public void BuyOrderComplited(int marketId)
        {
            this.orderCandidates[GetBuyKey(marketId)] = null;
        }

        public void SellOrderComplited(int marketId)
        {
            this.orderCandidates[GetSellKey(marketId)] = null;
        }

        private string GetKey(int marketId, OrderType type)
        {
            string typeName = string.Empty;
            if (type == OrderType.Buy)
            {
                typeName = "buy";
            }
            else
            {
                typeName = "sell";
            }

            return $"{typeName}{marketId}";
        }

        private class OrderCandidate
        {
            public Guid Id { get; set; }

            public int MarketId { get; set; }

            public OrderType OrderType { get; set; }

            public double Quantity { get; set; }

            public double Rate { get; set; }
        }
    }
}