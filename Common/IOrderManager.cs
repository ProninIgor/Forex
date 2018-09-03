using Common;
using Common.Data;

namespace Bittrex.Core
{
    /// <summary>
    /// Менеджер ставок. Хранит в себе инфу по ставкам, принимает решения о том, как расчитать магазин
    /// </summary>
    public interface IOrderManager
    {
        /// <summary>
        /// Есть ли ставка на покупку для указанного магазина
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        bool HasBuyOrder(int marketId);

        /// <summary>
        /// Есть ли ставка на продажу для указанного магазина
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

        /// <summary>
        /// Получить ставку на продажу
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        OrderDTO GetSellOrder(int marketId);

        void BuyOrderComplited(int marketId);

        void SellOrderComplited(int marketId);


        void Working();

        IStockExcangeObjectManager StockExcangeObjectManager { get; set; }
    }
}