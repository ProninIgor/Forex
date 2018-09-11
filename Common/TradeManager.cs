using System;
using Bittrex.Core;
using Common.Interfaces;

namespace Common
{
    public class TradeManager : ITradeManager
    {
        public IRealTimeData RealTimeData { get; }
        public bool SetBuyOrder(int marketId, decimal quality, decimal rate)
        {
            Console.WriteLine($"{marketId}-{quality}-{rate}");
            return true;
        }

        public bool SetSellOrder(int marketId, decimal quality, decimal rate)
        {
            //todo
            return true;
        }
    }
}