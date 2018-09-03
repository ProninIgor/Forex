using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    /// <summary>
    /// Маркет
    /// </summary>
    public class MarketDTO
    {
        /// <summary>
        /// ИД маркета
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Строковый код основного валюты маркета  
        /// </summary>
        public string MarketCurrency { get; set; }
        
        /// <summary>
        /// Строковый код базовой валюты маркета
        /// </summary>
        public string BaseCurrency { get; set; }
        
        /// <summary>
        /// ИД основной валюты маркета  
        /// </summary>
        public int MarketCurrencyId { get; set; }
        
        /// <summary>
        /// ИД базовой валюты маркета  
        /// </summary>
        public int BaseCurrencyId { get; set; }
        
        /// <summary>
        /// Минимальный объём ордера (в основной валюте)
        /// </summary>
        public double MinTradeSize { get; set; }
        
        /// <summary>
        /// Строковый код маркета
        /// </summary>
        public string MarketName { get; set; }
        
        /// <summary>
        /// Маркет доступен для торгов
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Дата создания маркета
        /// </summary>
        public DateTime Created { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public string Notice { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public bool? IsSponsored { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public string LogoUrl { get; set; }
    }
}
