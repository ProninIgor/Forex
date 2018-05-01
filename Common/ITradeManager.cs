namespace Bittrex.Core
{
    public interface ITradeManager
    {
        IRealTimeData RealTimeData { get; }

        bool SetBuyOrder(int marketId, double quality, double rate);
        bool SetSellOrder(int marketId, double quality, double rate);
    }
}