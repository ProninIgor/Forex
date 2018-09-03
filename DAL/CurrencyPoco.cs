using System;

namespace DAL
{
    /// <summary>
    /// POCO объект для Валюты
    /// </summary>
    [Table("Currencies")]
    public class CurrencyPoco
    {
        /// <summary>
        /// ИД валюты в БД
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Строковый код валюты (ADA, BTC, ETH)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Наименование валюты
        /// </summary>
        public string Name { get; set; }
    }
}