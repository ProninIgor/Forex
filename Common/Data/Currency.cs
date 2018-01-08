namespace Common.Data
{
    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int MinConfirmation { get; set; }
        public double TxFee { get; set; }
        public bool IsActive { get; set; }
        public string CoinType { get; set; }
        public object BaseAddress { get; set; }
    }
}