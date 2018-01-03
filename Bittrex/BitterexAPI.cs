using Common;

namespace Bittrex
{
    public class BitterexAPI
    {
        private const string ApiKey_v1_1 = "https://bittrex.com/api/v1.1/public";
        private const string ApiKey_v2_0 = "https://bittrex.com/Api/v2.0/pub";

        public string GetMarkets()
        {
            string marketAddress = $"{ApiKey_v1_1}/getmarkets";
            return GetResponse(marketAddress);
        }

        public string GetCurrencies()
        {
            string currenciesAddress = $"{ApiKey_v2_0}/getcurrencies";
            return GetResponse(currenciesAddress);
        }

        public string GetTicker(string market)
        {
            string tickerAddress = $"{ApiKey_v2_0}/{market}";
            return GetResponse(tickerAddress);
        }

        public string GetMarketSummaries()
        {
            string marketSummariesAddress = $"{ApiKey_v1_1}/getmarketsummaries";
            return GetResponse(marketSummariesAddress);
        }

        public string GetMarketSummary(string market)
        {
            string marketSummaryAddress = $"{ApiKey_v2_0}/getmarketsummary?market={market}";
            return GetResponse(marketSummaryAddress);
        }

        public string GetTicks(string marketName, string tickInterval)
        {
            string tickAddress = $"{ApiKey_v2_0}/market/GetTicks?marketName={marketName}&tickInterval={tickInterval}";
            return GetResponse(tickAddress);
        }

        private string GetResponse(string address)
        {
            WebManager webManager = new WebManager();
            return webManager.GetStringResponse(address);
        }
    }
}