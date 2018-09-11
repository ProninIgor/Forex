using Common.Interfaces;

namespace Bittrex.Core
{
    /// <summary>
    /// Контракт менеджера трейдера
    /// </summary>
    public interface ITradeManager
    {
        IRealTimeData RealTimeData { get; }

        /// <summary>
        /// Устанавливает ордер на покупку
        /// </summary>
        /// <param name="marketId"></param>
        /// <param name="quality"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        bool SetBuyOrder(int marketId, decimal quality, decimal rate);
        
        /// <summary>
        /// Устанавливает ордер на продажу
        /// </summary>
        /// <param name="marketId"></param>
        /// <param name="quality"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        bool SetSellOrder(int marketId, decimal quality, decimal rate);
    }
}