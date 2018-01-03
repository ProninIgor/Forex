using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "RootObject")]
    public class TickRootPoco
    {
        public bool success { get; set; }
        public string message { get; set; }

        [JsonProperty(PropertyName = "result")]
        public List<TickPoco> TickPocos { get; set; }
    }
}