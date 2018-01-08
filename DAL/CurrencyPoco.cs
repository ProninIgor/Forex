using System;

namespace DAL
{
    [Table("Currencies")]
    public class CurrencyPoco
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}