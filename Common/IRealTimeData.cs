using Common.Data;

namespace Bittrex.Core
{
    public interface IRealTimeData
    {
        OrderDTO GetAccountOrders(int marketId);

    }
}