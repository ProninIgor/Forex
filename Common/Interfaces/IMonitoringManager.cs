using Bittrex.Core;

namespace Common.Interfaces
{
    /// <summary>
    /// Главный менеджер для работы со ставками
    /// </summary>
    public interface IMonitoringManager
    {
        IOrderManager OrderManager { get; }

        ITradeManager TradeManager { get;}

        void Init();
        void Start();
        void Stop();
        void Abort();
    }


    internal class MarketOrderStatus
    {
        public MarketOrderStatus()
        {
            this.IsActive = true;
        }
        public bool IsActive { get; set; }
        public bool IsFindSellOrder { get; set; }
        public bool IsFindBuyOrder { get; set; }
        public bool IsHaveOneBuyOrder { get; set; }
        public bool IsHaveOneSellOrder { get; set; }
        
    }
}