using Common.Data;

namespace Common.Interfaces
{
    /// <summary>
    /// �������� ������. ������ � ���� ���� �� �������, ��������� ������� � ���, ��� ��������� �������
    /// </summary>
    public interface IOrderManager
    {
        /// <summary>
        /// ���� �� ������ �� ������� ��� ���������� ��������
        /// </summary>
        /// <param name="marketId"></param>
        /// <returns></returns>
        bool HasBuyOrder(int marketId);

        /// <summary>
        /// ���� �� ������ �� ������� ��� ���������� ��������
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

        /// <summary>
        /// �������� ������ �� �������
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