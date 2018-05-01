namespace Common
{
    public interface IStockExchangeAPI
    {
        string GetMarkets();
        string GetCurrencies();
        string GetTicker(string market);
        string GetMarketSummaries();
        string GetMarketSummary(string market);
        string GetTicks(string marketName, string tickInterval);
        string GetOpenOrders(string apiKey, string marketName);
    }
}