using System;
using System.Collections.Generic;
using System.Linq;
using Bittrex.JsonData;
using Common;
using Common.Data;
using Common.Entities;
using Newtonsoft.Json;

namespace Bittrex
{
    public class BittrexService
    {
        public void Go()
        {
            //var webManager = new WebManager();
            //string uri = @"https://bittrex.com/Api/v2.0/pub/market/GetLatestTick?marketName=BTC-XRP&tickInterval=hour";
            //string response = webManager.GetStringResponse(uri);
            //RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(response);

            BitterexAPI api = new BitterexAPI();
            string markets = api.GetMarkets();
            MarketRootJson marketRootPoco = JsonConvert.DeserializeObject<MarketRootJson>(markets);

            string marketSummaries = api.GetMarketSummaries();
            MarketSummaryRootJson marketSummaryRootJson = JsonConvert.DeserializeObject<MarketSummaryRootJson>(marketSummaries);
            Dictionary<string, double> volumes = marketSummaryRootJson.ItemJsons.ToDictionary(x=>x.MarketName, x=>x.BaseVolume);

            Dictionary<string, IEnumerable<TickJson>> responcies = new Dictionary<string, IEnumerable<TickJson>>();
            List<Period> periods = new List<Period>();
            int i = DateTime.Today.Day - 3;
            BitterexObjectManager bom = new BitterexObjectManager();
            foreach (MarketJson marketPoco in marketRootPoco.ItemJsons.Where(x=>x.BaseCurrency == "BTC" && x.IsActive))
            {
                if (marketPoco.MarketName == "BTC-DOGE")
                {
                }

                //if(volumes[marketPoco.MarketName] >= 3000)
                //    continue;

                if (volumes[marketPoco.MarketName] < 200)
                    continue;
//
//                List<TickDTO> ticks = bom.GetLastTicks(marketPoco., PeriodType.ThirtyMin, new TimeSpan(-10, 0, 0, 0)); //thirtyMin oneMin
//                Period period = new Period();
//                period.MarketName = marketPoco.MarketName;
//                period.Ticks = ticks;
//                period.LastVolume = volumes[marketPoco.MarketName];
//                periods.Add(period);
                //string ticks = api.GetTicks(marketPoco.MarketName, "thirtyMin");
                //TickRootJson tickRootPoco = JsonConvert.DeserializeObject<TickRootJson>(ticks);
                //List<TickJson> tickPocos = tickRootPoco.ItemJsons.Where(x=> x.DateTime.Day > i).ToList();
                //responcies[marketPoco.MarketName] = tickPocos;
                //double min = tickPocos.MinAvg(x => x.Value);
                //double max = tickPocos.MaxAvg(x => x.Value);
                //double last = tickPocos[tickPocos.Count - 1].Value;
                //double avg = tickPocos.Average(x => x.Value);
                //Console.WriteLine($"{marketPoco.MarketName}. min: {FormatValue(min)} max: {FormatValue(max)} last: {FormatValue(last)} avg: {FormatValue(avg)}");
            }

            foreach (Period period in periods.OrderBy(x => x.SuperDown))
            {
                Console.WriteLine(period.ToStringSuperDown());
            }

            foreach (Period period in periods.OrderBy(x => x.Mark1))
            {
                Console.WriteLine(period.ToStringMark1());
            }


            Console.WriteLine("-------------------------");

            //foreach (Period period in periods.OrderBy(x => x.MinDelta))
            //{
            //    Console.WriteLine(period.ToStringMinDelta());
            //}

            Console.ReadKey();

        }

       
    }
}