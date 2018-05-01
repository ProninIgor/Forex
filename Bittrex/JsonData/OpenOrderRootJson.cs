using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "RootObject")]
    public class OpenOrderRootJson : IItemJsons<OpenOrderJson>
    {
        public bool success { get; set; }
        public string message { get; set; }

        [JsonProperty(PropertyName = "result")]
        public List<OpenOrderJson> ItemJsons { get; set; }
    }
}