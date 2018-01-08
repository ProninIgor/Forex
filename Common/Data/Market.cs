using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    public class Market
    {
        public int Id { get; set; }
        public string MarketCurrency { get; set; }
        public string BaseCurrency { get; set; }
        public int MarketCurrencyId { get; set; }
        public int BaseCurrencyId { get; set; }
        public double MinTradeSize { get; set; }
        public string MarketName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public string Notice { get; set; }
        public bool? IsSponsored { get; set; }
        public string LogoUrl { get; set; }
    }
}
