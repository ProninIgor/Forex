using System;

namespace Common.Data
{
    public class TickDTO
    {
        private decimal value;
        private decimal delta;

        /// <summary>
        /// Значение при открытие тика
        /// </summary>
        public decimal OpenValue { get; set; }

        /// <summary>
        /// Максимальное значение за тик
        /// </summary>
        public decimal HighValue { get; set; }

        /// <summary>
        /// Минимальное значение за тик
        /// </summary>
        public decimal LowValue { get; set; }

        /// <summary>
        /// Значение при закрытии тика
        /// </summary>
        public decimal CloseValue { get; set; }

        /// <summary>
        /// Объём торгов при тике
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Дата ничала тика
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Объём тика в биткойнах
        /// </summary>
        public decimal BitcoinValue { get; set; }

        /// <summary>
        /// Медиана тика
        /// </summary>
        public decimal Value
        {
            get
            {
                return Math.Abs(value) < 0.0000000001m
                    ? (this.value = (this.OpenValue + this.CloseValue) / 2)
                    : this.value;
            }
        }

        /// <summary>
        /// Изменение за тик. Дельта в долевом значении
        /// </summary>
        public decimal Delta {
            get
            {
                return Math.Abs(delta) < 0.0000000001m
                 ? (this.delta = (this.CloseValue - this.OpenValue) / this.OpenValue)
                 : this.delta;
            }
        }
    }
}