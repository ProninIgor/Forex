using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    /// <summary>
    /// Секция для открытия ордера на покупку 
    /// </summary>
    public class StakeSectionBuy
    {
        /// <summary>
        /// Минимальная ставка для ордера
        /// </summary>
        public double MinRate { get; set; }

        /// <summary>
        /// Максимальное ставка для ордера
        /// </summary>
        public double MaxRate { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="minRate">Минимальная ставка для ордера</param>
        /// <param name="maxRate">Максимальное ставка для ордера</param>
        public StakeSectionBuy(double minRate, double maxRate)
        {
            MinRate = minRate;
            MaxRate = maxRate;
        }

        /// <summary>
        /// Проверка вхождения ставки в диапазон
        /// </summary>
        /// <param name="rate">ставка</param>
        /// <returns></returns>
        public bool InSection(double rate)
        {
            return this.MinRate <= rate && rate <= this.MaxRate;
        }
    }
}
