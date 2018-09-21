namespace Common.Entities
{
    /// <summary>
    /// Секция для открытия ордера 
    /// </summary>
    public class StakeSection
    {
        /// <summary>
        /// Минимальная ставка для ордера
        /// </summary>
        public decimal MinRate { get; set; }

        /// <summary>
        /// Максимальное ставка для ордера
        /// </summary>
        public decimal MaxRate { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="minRate">Минимальная ставка для ордера</param>
        /// <param name="maxRate">Максимальное ставка для ордера</param>
        public StakeSection(decimal minRate, decimal maxRate)
        {
            MinRate = minRate;
            MaxRate = maxRate;
        }

        /// <summary>
        /// Проверка вхождения ставки в диапазон
        /// </summary>
        /// <param name="rate">ставка</param>
        /// <returns></returns>
        public bool InSection(decimal rate)
        {
            return this.MinRate <= rate && rate <= this.MaxRate;
        }
        
        /// <summary>
        /// Проверка вхождения ставки в диапазон
        /// </summary>
        /// <param name="rate">ставка</param>
        /// <returns></returns>
        public bool InSectionSell(decimal rate)
        {
            return this.MinRate <= rate ;
        }
    }
}