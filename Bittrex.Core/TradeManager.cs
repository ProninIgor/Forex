namespace Bittrex.Core
{
    public class TradeManager : ITradeManager
    {
        public IRealTimeData RealTimeData { get; }
        public bool SetBuyOrder(int marketId, double quality, double rate)
        {
            //todo
            return true;
        }

        public bool SetSellOrder(int marketId, double quality, double rate)
        {
            //todo
            return true;
        }
    }
}