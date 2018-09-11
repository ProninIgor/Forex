using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;

namespace Bittrex
{
    public class Period
    {
        private decimal avg;
        private decimal min;
        private decimal minAvg;
        private decimal max;
        private decimal maxAvg;


        public string MarketName { get; set; }

        public decimal LastVolume { get; set; }

        public decimal Min
        {
            get { return Math.Abs(this.min) < 0.0000000001m ? (this.min = this.Ticks.Min(x => x.LowValue)) : this.min; }
        }

        public decimal Max
        {
            get { return Math.Abs(this.max) < 0.0000000001m ? (this.max = this.Ticks.Max(x => x.HighValue)) : this.max; }

        }

        public decimal MinAvg
        {
            get { return Math.Abs(this.minAvg) < 0.0000000001m ? (this.minAvg = this.Ticks.Average(x => x.LowValue)) : this.minAvg; }
        }

        public decimal MaxAvg
        {
            get { return Math.Abs(this.maxAvg) < 0.0000000001m ? (this.maxAvg = this.Ticks.Average(x => x.HighValue)) : this.maxAvg; }
            
        }

        public decimal Last
        {
            get { return this.Ticks[this.Ticks.Count - 1].CloseValue; }
        }

        public decimal Avg
        {
            get { return Math.Abs(this.avg) < 0.0000000001m ? (this.avg = this.Ticks.Average(x => x.Value)) : this.avg; }
        }

        public decimal Mark1 {
            get { return (this.Avg - this.Last) / this.Avg; }
        }

        public decimal MinDelta
        {
            get { return (this.Max - this.Min) / this.Min; }
        }

        public decimal SuperDown
        {
            get
            {
                decimal result = 0;
                for (int i = 1; i < 5; i++)
                {
                    result += this.Ticks[this.Ticks.Count - i].Delta;
                }

                return result;
            }
        }

        public List<TickDTO> Ticks { get; set; }

        public string ToStringMark1()
        {
            return
                $"{this.MarketName}. minA: {FormatValue(this.MinAvg)} maxA: {FormatValue(MaxAvg)} last: {FormatValue(this.Last)} avg: {FormatValue(this.Avg)}";
        }

        public string ToStringMinDelta()
        {
            return
                $"{this.MarketName}. min: {FormatValue(this.Min)} max: {FormatValue(Max)} last: {FormatValue(this.Last)} avg: {FormatValue(this.Avg)}";
        }

        public string ToStringSuperDown()
        {
            return
                $"{this.MarketName}. min: {FormatValue(this.Min)} max: {FormatValue(Max)} last: {FormatValue(this.Last)} down: {FormatValue(this.SuperDown)} vol {LastVolume}";
        }

        private string FormatValue(decimal val)
        {
            return val.ToString("0.00000000");
        }
    }
}