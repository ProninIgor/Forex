using System;

namespace DAL
{
    /// <summary>
    /// Сводная информация по маркету за последние сутки
    /// </summary>
    [Table("MarketSummaries")]
    public class MarketSummaryPoco
    {
        /// <summary>
        /// ИД сводной информации по маркету
        /// </summary>
        [Key]
        public int Id { get; set; }
        
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
        public double High { get; set; }
        
        /// <summary>
        /// Минимальное значение в базовой валюте
        /// </summary>
        public double Low { get; set; }
        
        /// <summary>
        /// Объём торгов по маркету
        /// </summary>
        public double Volume { get; set; }
        
        /// <summary>
        /// Последнее значение по маркету
        /// </summary>
        public double Last { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public double BaseVolume { get; set; }
        
        /// <summary>
        /// ??? Временной штамп информации
        /// </summary>
        public DateTime TimeStamp { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public double Bid { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public double Ask { get; set; }
        
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
        public double PrevDay { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public DateTime Created { get; set; }
    }
}