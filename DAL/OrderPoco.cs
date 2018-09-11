using System;
using DAL;
using Enums;

namespace DAL
{
    /// <summary>
    /// ПОКО для Ордера
    /// </summary>
    [Table("Orders")]
    public class OrderPoco
    {
        /// <summary>
        /// ИД ордера
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// ИД маркета
        /// </summary>
        public int MarketId { get; set; }
        
        /// <summary>
        /// Тип ставки (покупка/продажа)
        /// </summary>
        public OrderType OrderType { get; set; }
        
        /// <summary>
        /// Ставка (в основной валюте)
        /// </summary>
        public decimal Rate { get; set; }
        
        /// <summary>
        /// Объём (в основной валюте)
        /// </summary>
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public decimal BaseVolume { get; set; }
        
        /// <summary>
        /// Время, когда была выставлена ставка
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}