using Common.Data;

namespace Common.Interfaces
{
    /// <summary>
    /// �������� ��������� ������ � �������� �������
    /// </summary>
    public interface IRealTimeData
    {
        OrderDTO GetAccountOrders(int marketId);
    }
}