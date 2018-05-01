namespace Bittrex.Core
{
    class TradeManager : ITradeManager
    {
        public IRealTimeData RealTimeData { get; }
        public void SetBuyOrder(int marketId, double quality, double rate)
        {
            throw new System.NotImplementedException();
        }

        public void SetSellOrder(int marketId, double quality, double rate)
        {
            throw new System.NotImplementedException();
        }
    }
}