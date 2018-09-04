using Common.Data;

namespace Common.Interfaces
{
    /// <summary>
    /// Контракт получения данных в реальном времени
    /// </summary>
    public interface IRealTimeData
    {
        OrderDTO GetAccountOrders(int marketId);
    }
}