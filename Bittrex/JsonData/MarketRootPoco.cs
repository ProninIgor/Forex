﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bittrex.JsonData
{
   
        [JsonObject(Title = "RootObject")]
        public class MarketRootPoco
        {
            public bool success { get; set; }
            public string message { get; set; }
            
            [JsonProperty(PropertyName = "result")]
            public List<MarketPoco> MarketPocos { get; set; }
        }
    
}