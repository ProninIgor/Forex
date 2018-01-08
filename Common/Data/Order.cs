using Enums;

namespace Common.Data
{
    public class Order
    {
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public OrderType OrderType { get; set; }
    }
}