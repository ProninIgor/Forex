using Common.Data;

namespace Bittrex.Core
{
    public interface IRealTimeData
    {
        Order GetAccountOrders(int marketId);

    }
}