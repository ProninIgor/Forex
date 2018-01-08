using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "Sell")]
    public class SellJson
    {
        public double Quantity { get; set; }
        public double Rate { get; set; }
    }
}