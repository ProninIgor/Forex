using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;

namespace Bittrex
{
    public class Period
    {
        private double avg;
        private double min;
        private double minAvg;
        private double max;
        private double maxAvg;


        public string MarketName { get; set; }

        public double LastVolume { get; set; }

        public double Min
        {
            get { return Math.Abs(this.min) < 0.0000000001 ? (this.min = this.Ticks.Min(x => x.LowValue)) : this.min; }
        }

        public double Max
        {
            get { return Math.Abs(this.max) < 0.0000000001 ? (this.max = this.Ticks.Max(x => x.HighValue)) : this.max; }

        }

        public double MinAvg
        {
            get { return Math.Abs(this.minAvg) < 0.0000000001 ? (this.minAvg = this.Ticks.Average(x => x.LowValue)) : this.minAvg; }
        }

        public double MaxAvg
        {
            get { return Math.Abs(this.maxAvg) < 0.0000000001 ? (this.maxAvg = this.Ticks.Average(x => x.HighValue)) : this.maxAvg; }
            
        }

        public double Last
        {
            get { return this.Ticks[this.Ticks.Count - 1].CloseValue; }
        }

        public double Avg
        {
            get { return Math.Abs(this.avg) < 0.0000000001 ? (this.avg = this.Ticks.Average(x => x.Value)) : this.avg; }
        }

        public double Mark1 {
            get { return (this.Avg - this.Last) / this.Avg; }
        }

        public double MinDelta
        {
            get { return (this.Max - this.Min) / this.Min; }
        }

        public double SuperDown
        {
            get
            {
                double result = 0;
                for (int i = 1; i < 5; i++)
                {
                    result += this.Ticks[this.Ticks.Count - i].Delta;
                }

                return result;
            }
        }

        public List<Tick> Ticks { get; set; }

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

        private string FormatValue(double val)
        {
            return val.ToString("0.00000000");
        }
    }
}