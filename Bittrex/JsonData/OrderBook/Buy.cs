using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "Buy")]
    public class BuyJson
    {
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}