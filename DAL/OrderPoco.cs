using System;
using DAL;
using Enums;

namespace DAL
{
    [Table("Orders")]
    public class OrderPoco
    {
        [Key]
        public int Id { get; set; }
        public int MarketId { get; set; }
        public OrderType OrderType { get; set; }
        public double Rate { get; set; }
        public double Quantity { get; set; }
        public double BaseVolume { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}