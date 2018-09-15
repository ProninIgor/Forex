using Common.Data;

namespace Common.Interfaces
{
    /// <summary>
    /// �������� ������. ������ � ���� ���� �� �������, ��������� ������� � ���, ��� ����������� ������
    /// </summary>
    public interface IOrderManager
    {
        /// <summary>
        /// ���� �� ������������ ������ �� ������� ��� ���������� ��������
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        bool HasBuyOrder(int marketId);

        /// <summary>
        /// ���� �� ������������ ������ �� ������� ��� ���������� ��������
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        bool HasSellOrder(int marketId);

        /// <summary>
        /// �������� ������ �� �������
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        OrderDTO GetBuyOrder(int marketId);

        bool IsFindOrderBuy(int marketId);

        bool IsFindOrderSell(int marketId);
        
        void SetOrderBuy(int marketId);

        void SetOrderSell(int marketId);

        /// <summary>
        /// �������� ������ �� �������
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