using System;

namespace DAL
{
    /// <summary>
    /// POCO для маркета
    /// </summary>
    [Table("Markets")]
    public class MarketPoco
    {
        /// <summary>
        /// ИД маркета
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// ИД основной валюты маркета
        /// </summary>
        public int MarketCurrencyId { get; set; }
        
        /// <summary>
        /// ИД базовой валюты маркета. Чаще всего это btc, eth, usdt
        /// </summary>
        public int BaseCurrencyId { get; set; }
        
        /// <summary>
        /// Минимальная ставка по маркету в основной валюте 
        /// </summary>
        public double MinTradeSize { get; set; }
        
        /// <summary>
        /// Строковое название маркета BTC-ADA, BTC-ETH
        /// </summary>
        public string MarketName { get; set; }
        
        /// <summary>
        /// Маркет доступен для торговли
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Дата создания маркета на бирже
        /// </summary>
        public DateTime Created { get; set; }
    }
}