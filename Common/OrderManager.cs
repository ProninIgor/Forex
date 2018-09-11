using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Bittrex.Core;
using Common.Data;
using Common.Interfaces;
using Enums;

namespace Common
{
    /// <summary>
    /// Менеджер ставок. Хранит в себе инфу по ставкам, принимает решения о том, как расчитывать ставки
    /// </summary>
    public partial class OrderManager : IOrderManager
    {
        //todo: Сделать возможным только один кандижат на маркет и тип. Или нотифицировать по Guid для идентификации кандидата
        private ConcurrentDictionary<string, OrderCandidate> orderCandidates = new ConcurrentDictionary<string, OrderCandidate>();
        private int[] marketIds;
        private IAnalizeManager AnalizeManager { get; set; }
        public IStockExcangeObjectManager StockExcangeObjectManager { get; set; }
        public IObjectManager ObjectManager { get; }



        public OrderManager(int[] marketIds, IStockExcangeObjectManager stockExcangeObjectManager, IObjectManager objectManager)
        {
            this.marketIds = marketIds;
            foreach (int marketId in marketIds)
            {
                string key = GetBuyKey(marketId);
                orderCandidates.TryAdd(key, null);
                key = GetSellKey(marketId);
                orderCandidates.TryAdd(key, null);
            }
            this.AnalizeManager = new AnalizeManager(objectManager);
            this.StockExcangeObjectManager = stockExcangeObjectManager;
            this.ObjectManager = objectManager;
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

        public OrderDTO GetBuyOrder(int marketId)
        {
            OrderCandidate orderCandidate = this.orderCandidates[GetBuyKey(marketId)];

            OrderDTO buyOrderDto = orderCandidate == null 
                ? null 
                : new OrderDTO(){OrderType = OrderType.Buy, Quantity = orderCandidate.Quantity, Rate = orderCandidate.Rate};

            return buyOrderDto;
        }

        public OrderDTO GetSellOrder(int marketId)
        {
            OrderCandidate orderCandidate = this.orderCandidates[GetSellKey(marketId)];

            OrderDTO sellOrderDto = orderCandidate == null
                ? null
                : new OrderDTO() { OrderType = OrderType.Sell, Quantity = orderCandidate.Quantity, Rate = orderCandidate.Rate };

            return sellOrderDto;
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

            public decimal Quantity { get; set; }

            public decimal Rate { get; set; }
        }
    }
}