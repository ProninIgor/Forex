using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Bittrex.Core;
using Common.Data;
using Common.Entities;
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
        private Dictionary<int, MarketOrderStatus> _marketOrderStatuses = new Dictionary<int, MarketOrderStatus>();
        
        private Dictionary<int, OrderBO> _orderBos = new Dictionary<int, OrderBO>();
        private Dictionary<int, OrderDTO> _marketOrders = new Dictionary<int, OrderDTO>();
        private int[] marketIds;
        private IAnalizeManager AnalizeManager { get; set; }
        public IStockExcangeObjectManager StockExcangeObjectManager { get; set; }
        public IObjectManager ObjectManager { get; }



        public OrderManager(int[] marketIds, IStockExcangeObjectManager stockExcangeObjectManager, IObjectManager objectManager)
        {
            this.marketIds = marketIds;
            foreach (int marketId in marketIds)
            {
                _orderBos.Add(marketId, new OrderBO(){Status = OrderBOStatus.Start});
                string key = GetBuyKey(marketId);
                orderCandidates.TryAdd(key, null);
                key = GetSellKey(marketId);
                orderCandidates.TryAdd(key, null);
                _marketOrderStatuses.Add(marketId, new MarketOrderStatus());
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
            return _orderBos[marketId].Status == OrderBOStatus.SetOrderBuy;
        }

        public bool HasSellOrder(int marketId)
        {
            return _orderBos[marketId].Status == OrderBOStatus.SetOrderSell;
        }
        
        public bool IsFindOrderBuy(int marketId)
        {
            return GetOrderBo(marketId).Status == OrderBOStatus.FindOrderBuy;
        }

        private OrderBO GetOrderBo(int marketId)
        {
            return _orderBos[marketId];
        }

        public bool IsFindOrderSell(int marketId)
        {
            return _orderBos[marketId].Status == OrderBOStatus.FindOrderSell;
        }

        public OrderDTO GetBuyOrder(int marketId)
        {
            OrderCandidate orderCandidate = _orderBos[marketId].OrderCandidateBuy;

            OrderDTO buyOrderDto = orderCandidate == null 
                ? null 
                : new OrderDTO(){OrderType = OrderType.Buy, Quantity = orderCandidate.Quantity, Rate = orderCandidate.Rate};

            return buyOrderDto;
        }

        public OrderDTO GetSellOrder(int marketId)
        {
            OrderCandidate orderCandidate = _orderBos[marketId].OrderCandidateSell;

            OrderDTO sellOrderDto = orderCandidate == null
                ? null
                : new OrderDTO() { OrderType = OrderType.Sell, Quantity = orderCandidate.Quantity, Rate = orderCandidate.Rate };

            return sellOrderDto;
        }

        public void SetOrderBuy(int marketId)
        {
            var orderBo = this.GetOrderBo(marketId);
            orderBo.Quantity = orderBo.OrderCandidateBuy.Quantity;
            orderBo.Rate = orderBo.OrderCandidateBuy.Rate;
            orderBo.Status = OrderBOStatus.SetOrderBuy;
        }

        public void SetOrderSell(int marketId)
        {
            var orderBo = this.GetOrderBo(marketId);
            orderBo.Status = OrderBOStatus.SetOrderSell;
        }

        public void BuyOrderCompleted(int marketId)
        {
            var orderBo = this.GetOrderBo(marketId);
            orderBo.Status = OrderBOStatus.CompliteBuy;
        }

        public void SellOrderCompleted(int marketId)
        {
            var orderBo = this.GetOrderBo(marketId);
            orderBo.Status = OrderBOStatus.CompliteSell;
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
    }
}