using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "RootObject")]
    public class OrderBookRootJson
    {
        public bool success { get; set; }
        public string message { get; set; }

        [JsonProperty(PropertyName = "result")]
        public OrderBookJson OrderBookJson { get; set; }
    }
}