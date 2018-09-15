using System;
using Enums;

namespace Common.Entities
{

    /// <summary>
    /// Кандидат на ставку 
    /// </summary>
    public class OrderCandidate
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Магазин, для которого надо сделать ставку
        /// </summary>
        public int MarketId { get; set; }

        /// <summary>
        /// Тип ставки (Sell, Buy)
        /// </summary>
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Объъём ставки
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Цена ставки в основной валюте
        /// </summary>
        public decimal Rate { get; set; }
    }

}