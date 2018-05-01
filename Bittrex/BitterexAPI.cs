using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Common;

namespace Bittrex
{
    public class BitterexAPI : IStockExchangeAPI
    {
        private const string ApiKeyPublic_v1_1 = "https://bittrex.com/api/v1.1/public";
        private const string ApiKeyPublic_v2_0 = "https://bittrex.com/Api/v2.0/pub";
        private const string ApiKeyMarket_v1_1 = "https://bittrex.com/api/v1.1/market";
        private const string ApiKeyAccount_v1_1 = "https://bittrex.com/api/v1.1/account";

        private string secret = "fca0e89a0b2549838d81ac6c242a0a2e";

        public string GetMarkets()
        {
            string marketAddress = $"{ApiKeyPublic_v1_1}/getmarkets";
            return GetResponse(marketAddress);
        }

        public string GetCurrencies()
        {
            string currenciesAddress = $"{ApiKeyPublic_v1_1}/getcurrencies";
            return GetResponse(currenciesAddress);
        }

        public string GetTicker(string market)
        {
            string tickerAddress = $"{ApiKeyPublic_v2_0}/{market}";
            return GetResponse(tickerAddress);
        }

        public string GetMarketSummaries()
        {
            string marketSummariesAddress = $"{ApiKeyPublic_v1_1}/getmarketsummaries";
            return GetResponse(marketSummariesAddress);
        }

        public string GetOrderBooks(string market, string type = "both")
        {
            string orderBookAddress = $"{ApiKeyPublic_v1_1}/getorderbook?market={market}&type={type}";
            return GetResponse(orderBookAddress);
        }

        public string GetMarketSummary(string market)
        {
            string marketSummaryAddress = $"{ApiKeyPublic_v2_0}/getmarketsummary?market={market}";
            return GetResponse(marketSummaryAddress);
        }

        public string GetTicks(string marketName, string tickInterval)
        {
            string tickAddress = $"{ApiKeyPublic_v2_0}/market/GetTicks?marketName={marketName}&tickInterval={tickInterval}";
            return GetResponse(tickAddress);
        }

        public string GetOpenOrders(string apiKey, string marketName)
        {
            long nonce = DateTime.Now.Ticks;
            string openOredr = $"{ApiKeyMarket_v1_1}/getopenorders?apikey={apiKey}&nonce={nonce}&market={marketName}";
            string hashHmac = HashHmac(openOredr, this.secret);
            List<Header> headers = new List<Header>() {new Header() {Name = "apisign", Value = hashHmac}};

            return GetResponse(openOredr, headers);
        }

        private static string HashHmac(string message, string secret)
        {
            Encoding encoding = Encoding.UTF8;
            using (HMACSHA512 hmac = new HMACSHA512(encoding.GetBytes(secret)))
            {
                var msg = encoding.GetBytes(message);
                var hash = hmac.ComputeHash(msg);
                return BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
            }
        }

        private string GetResponse(string address, IEnumerable<Header> headers)
        {
            WebManager webManager = new WebManager();
            foreach (Header header in headers)
            {
                webManager.AddHeader(header.Name, header.Value);
            }
            return webManager.GetStringResponse(address);
        }

        private string GetResponse(string address)
        {
            WebManager webManager = new WebManager();
            return webManager.GetStringResponse(address);
        }

        private class Header
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}