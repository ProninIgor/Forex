using System;

namespace Common.Data
{
    /// <summary>
    /// Сводная информация по маркету за последние сутки
    /// </summary>
    public class MarketSummaryDTO
    {
        /// <summary>
        /// ИД маркета 
        /// </summary>
        public int MarketId { get; set; }
        
        /// <summary>
        /// Строковое название маркета BTC-ADA, BTC-ETH
        /// </summary>
        public string MarketName { get; set; }
        
        /// <summary>
        /// Максимальное значение в базовой валюте
        /// </summary>
        public decimal High { get; set; }
        
        /// <summary>
        /// Минимальное значение в базовой валюте
        /// </summary>
        public decimal Low { get; set; }
        
        /// <summary>
        /// Объём торгов по маркету в основной валюте
        /// </summary>
        public decimal Volume { get; set; }
        
        /// <summary>
        /// Последнее значение по маркету
        /// </summary>
        public decimal Last { get; set; }
        
        /// <summary>
        /// Объём торгов по маркету в основной валюте
        /// </summary>
        public decimal BaseVolume { get; set; }
        
        /// <summary>
        /// ??? Временной штамп информации
        /// </summary>
        public DateTime TimeStamp { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public decimal Bid { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public decimal Ask { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public int OpenBuyOrders { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public int OpenSellOrders { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public decimal PrevDay { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public DateTime Created { get; set; }
    }
}