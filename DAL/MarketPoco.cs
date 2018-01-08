using System;

namespace DAL
{
    [Table("Markets")]
    public class MarketPoco
    {
        [Key]
        public int Id { get; set; }
        public int MarketCurrencyId { get; set; }
        public int BaseCurrencyId { get; set; }
        public double MinTradeSize { get; set; }
        public string MarketName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}