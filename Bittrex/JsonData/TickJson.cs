using System;
using Newtonsoft.Json;

namespace Bittrex.JsonData
{
    [JsonObject(Title = "Result")]
    public class TickJson
    {
        private double _value;

        [JsonProperty(PropertyName = "O")]
        public double OpenValue { get; set; }

        [JsonProperty(PropertyName = "H")]
        public double HighValue { get; set; }

        [JsonProperty(PropertyName = "L")]
        public double LowValue { get; set; }

        [JsonProperty(PropertyName = "C")]
        public double CloseValue { get; set; }

        [JsonProperty(PropertyName = "V")]
        public double Volume { get; set; }

        [JsonProperty(PropertyName = "T")]
        public DateTime DateTime { get; set; }

        [JsonProperty(PropertyName = "BV")]
        public double BitcoinValue { get; set; }

        public double Value
        {
            get
            {
                return Math.Abs(_value) < 0.0000000001
                    ? (this._value = (this.OpenValue + this.CloseValue) / 2)
                    : this._value;
            }
        }
    }
}