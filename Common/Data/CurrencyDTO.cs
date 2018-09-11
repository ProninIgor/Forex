namespace Common.Data
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class CurrencyDTO
    {
        /// <summary>
        /// Строковый код валюты (ADA, BTC, ETH)
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Название валюты
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public int MinConfirmation { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public decimal TxFee { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public string CoinType { get; set; }
        
        /// <summary>
        /// ???
        /// </summary>
        public object BaseAddress { get; set; }
    }
}