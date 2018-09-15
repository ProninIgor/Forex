namespace Common.Entities
{
    /// <summary>
    /// Бизнес объект для жизненного цикла ставки. От нахождения значения до обратной продажи
    /// </summary>
    public class OrderBO
    {
        /// <summary>
        /// Ордер-кандидат на покупку основной валюты
        /// </summary>
        public OrderCandidate OrderCandidateBuy { get; set; }
        
        /// <summary>
        /// Ордер-кандидат на продажу основной валюты
        /// </summary>
        public OrderCandidate OrderCandidateSell { get; set; }

        /// <summary>
        /// Текущий статус ставки
        /// </summary>
        public OrderBOStatus Status { get; set; }
        
        /// <summary>
        /// Секция(коридор) для покупки основной валюты
        /// </summary>
        public StakeSection StakeSectionBuy { get; set; }
        
        /// <summary>
        /// Секция(коридор) для продажи основной валюты
        /// </summary>
        public StakeSection StakeSectionSell { get; set; }
        
        /// <summary>
        /// Тип расчёта, которым был получен кандидат на покупку
        /// </summary>
        public AlgorithmCalculateType CalculateType { get; set; }

        /// <summary>
        /// Объем ордера в основной валюте
        /// </summary>
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Цена покупки в основной валюте
        /// </summary>
        public decimal Rate { get; set; }
    }
}