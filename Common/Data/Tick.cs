using System;

namespace Common.Data
{
    public class Tick
    {
        private double value;
        private double delta;

        public double OpenValue { get; set; }

        public double HighValue { get; set; }

        public double LowValue { get; set; }

        public double CloseValue { get; set; }

        public double Volume { get; set; }

        public DateTime DateTime { get; set; }

        public double BitcoinValue { get; set; }

        public double Value
        {
            get
            {
                return Math.Abs(value) < 0.0000000001
                    ? (this.value = (this.OpenValue + this.CloseValue) / 2)
                    : this.value;
            }
        }

        public double Delta {
            get
            {
                return Math.Abs(delta) < 0.0000000001
                 ? (this.delta = (this.CloseValue - this.OpenValue) / this.OpenValue)
                 : this.delta;
            }
        }
    }
}