using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "RootObject")]
    public class MarketSummaryRootJson : IItemJsons<MarketSummaryJson>
    {
        public bool success { get; set; }
        public string message { get; set; }

        [JsonProperty(PropertyName = "result")]
        public List<MarketSummaryJson> ItemJsons { get; set; }
    }
}