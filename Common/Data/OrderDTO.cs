﻿using Enums;

namespace Common.Data
{
    public class OrderDTO
    {
        /// <summary>
        /// Объем торгов в основной валюте
        /// </summary>
        public double Quantity { get; set; }
        
        /// <summary>
        /// Ставка ордера
        /// </summary>
        public double Rate { get; set; }
        
        /// <summary>
        /// Тип ордера (покупка/продажа)
        /// </summary>
        public OrderType OrderType { get; set; }
    }
}