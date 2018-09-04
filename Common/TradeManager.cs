using System;
using Bittrex.Core;
using Common.Interfaces;

namespace Common
{
    public class TradeManager : ITradeManager
    {
        public IRealTimeData RealTimeData { get; }
        public bool SetBuyOrder(int marketId, double quality, double rate)
        {
            Console.WriteLine($"{marketId}-{quality}-{rate}");
            return true;
        }

        public bool SetSellOrder(int marketId, double quality, double rate)
        {
            //todo
            return true;
        }
    }
}