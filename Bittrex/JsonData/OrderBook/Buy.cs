using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "Buy")]
    public class BuyJson
    {
        public double Quantity { get; set; }
        public double Rate { get; set; }
    }
}