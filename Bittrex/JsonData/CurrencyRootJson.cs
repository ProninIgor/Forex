using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "RootObject")]
    public class CurrencyRootJson : IItemJsons<CurrencyJson>
    {
        public bool success { get; set; }
        public string message { get; set; }

        [JsonProperty(PropertyName = "result")]
        public List<CurrencyJson> ItemJsons { get; set; }
    }
}