using Common.Data;

namespace Common.Interfaces
{
    /// <summary>
    /// Менеджер ставок. Хранит в себе инфу по ставкам, принимает решения о том, как расчитывать ставки
    /// </summary>
    public interface IOrderManager
    {
        /// <summary>
        /// Есть ли поставленная ставка на покупку для указанного магазина
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        bool HasBuyOrder(int marketId);

        /// <summary>
        /// Есть ли поставленная ставка на продажу для указанного магазина
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        bool HasSellOrder(int marketId);

        /// <summary>
        /// Получить ставку на покупку
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        OrderDTO GetBuyOrder(int marketId);

        bool IsFindOrderBuy(int marketId);

        bool IsFindOrderSell(int marketId);
        
        void SetOrderBuy(int marketId);

        void SetOrderSell(int marketId);

        /// <summary>
        /// Получить ставку на продажу
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        OrderDTO GetSellOrder(int marketId);

        void BuyOrderCompleted(int marketId);

        void SellOrderCompleted(int marketId);

        void Working();

        //void StopFindBuyOrder(int marketId);

        IStockExcangeObjectManager StockExcangeObjectManager { get; set; }
    }
}