using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "Result")]
    public class OrderBookJson
    {
        [JsonProperty(PropertyName = "buy")]
        public List<BuyJson> Buys { get; set; }

        [JsonProperty(PropertyName = "sell")]
        public List<SellJson> Sells { get; set; }
    }
}
