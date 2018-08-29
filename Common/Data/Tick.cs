using System;

namespace Common.Data
{
    public class Tick
    {
        private double value;
        private double delta;

        /// <summary>
        /// Значение при открытие тика
        /// </summary>
        public double OpenValue { get; set; }

        /// <summary>
        /// Максимальное значение за тик
        /// </summary>
        public double HighValue { get; set; }

        /// <summary>
        /// Минимальное значение за тик
        /// </summary>
        public double LowValue { get; set; }

        /// <summary>
        /// Значение при закрытии тика
        /// </summary>
        public double CloseValue { get; set; }

        /// <summary>
        /// Объём торгов при тике
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Дата ничала тика
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Объём тика в биткойнах
        /// </summary>
        public double BitcoinValue { get; set; }

        /// <summary>
        /// Медиана тика
        /// </summary>
        public double Value
        {
            get
            {
                return Math.Abs(value) < 0.0000000001
                    ? (this.value = (this.OpenValue + this.CloseValue) / 2)
                    : this.value;
            }
        }

        /// <summary>
        /// Изменение за тик. Дельта в долевом значении
        /// </summary>
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