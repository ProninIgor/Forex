using System;
using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "Result")]
    public class TickJson
    {
        private decimal _value;

        [JsonProperty(PropertyName = "O")]
        public decimal OpenValue { get; set; }

        [JsonProperty(PropertyName = "H")]
        public decimal HighValue { get; set; }

        [JsonProperty(PropertyName = "L")]
        public decimal LowValue { get; set; }

        [JsonProperty(PropertyName = "C")]
        public decimal CloseValue { get; set; }

        [JsonProperty(PropertyName = "V")]
        public decimal Volume { get; set; }

        [JsonProperty(PropertyName = "T")]
        public DateTime DateTime { get; set; }

        [JsonProperty(PropertyName = "BV")]
        public decimal BitcoinValue { get; set; }

        public decimal Value
        {
            get
            {
                return Math.Abs(_value) < 0.0000000001m
                    ? (this._value = (this.OpenValue + this.CloseValue) / 2)
                    : this._value;
            }
        }
    }
}