﻿using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "Sell")]
    public class SellJson
    {
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}