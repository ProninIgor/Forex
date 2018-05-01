using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Bittrex;
using Bittrex.Core;
using Common;
using Common.Data;
using DAL;
using Enums;
using Newtonsoft.Json;
using RecipientData;

namespace Forex
{
    class Program
    {
        private class Message
        {
            public double Last { get; set; }
            public string MarketName { get; set; }
            public double BaseVolume { get; set; }
            public IEnumerable<Order> Buies { get; set; }
            public IEnumerable<Order> Sells { get; set; }
        }

        static void Main(string[] args)
        {
            MonitoringManager monitoringManager = new MonitoringManager(new[]{1, 2 , 3});
            monitoringManager.Init();
            monitoringManager.Start();


            /*
            Copier copier = new Copier(new BitterexObjectManager());
            //copier.StartCurrencies();
            //copier.StartMarkets();
            //copier.StartMarketSummaries();
            //return;
            //copier.StartOrders();
            ILogger logger = new EmptyLogger();
            ILogger logger2 = new FileLogger(@"D:\Фото\bittrex");
            IEnumerable<MarketPoco> marketPocos = Enumerable.Empty<MarketPoco>();

            using (ConnectionDb connection = new ConnectionDb())
            {
                marketPocos= connection.GetAll<MarketPoco>();
            }

            /*
             
             
             */
            //Exchange exchange = new Exchange();
            //ExchangeContext exchangeContext = new ExchangeContext();
            //exchangeContext.QuoteCurrency = "BTC";
            //exchangeContext.Simulate = false;
            //exchange.Initialise(exchangeContext);
            //GetOpenOrdersResponse openOrders1 = exchange.GetOpenOrders("NXC");

            
            /*IStockExcangeObjectManager objectManager = new BitterexObjectManager();
            //List<OpenOrder> openOrders = objectManager.GetOpenOrders("601659f153e344fb85dd106c6f5dfaad", "BTC-NXC");
            List<MarketSummary> marketSummaries = objectManager.GetMarketSummaries();
            ConcurrentDictionary<string, List<Order>> res = new ConcurrentDictionary<string, List<Order>>();
            Parallel.ForEach(marketSummaries, (marketSummary) =>
            {
                // foreach (MarketSummary marketSummary in marketSummaries)
                // {
                if (marketSummary.MarketName.Contains("RDD")
                    || marketSummary.MarketName.Contains("SC")
                    || marketSummary.MarketName.Contains("ETH")
                    || marketSummary.MarketName.Contains("DOGE")
                    || marketSummary.MarketName.Contains("DGB")
                )
                {
                    return;
                }

                MarketPoco marketPoco = marketPocos.FirstOrDefault(x => x.MarketName == marketSummary.MarketName);
                
                if (!marketPoco.IsActive || marketPoco.BaseCurrencyId != 2)
                {
                    return;
                }

                //if (marketSummary.BaseVolume < 100 || marketSummary.BaseVolume > 700)
                //{
                //    continue;
                //}

                List<Order> orders = objectManager.GetOrders(marketSummary.MarketName);
                res[marketSummary.MarketName] = orders;
                Console.WriteLine(marketSummary.MarketName);
            });
           // }


            List<Message> result = new List<Message>();
        foreach (var re in res)
            {
                MarketSummary marketSummary = marketSummaries.FirstOrDefault(x => x.MarketName == re.Key);

                Message mes = new Message();
                mes.MarketName = marketSummary.MarketName;
                mes.Last = marketSummary.Last;
                mes.BaseVolume = Math.Round(marketSummary.BaseVolume);

                
                IEnumerable<Order> Buies = re.Value.Where(x => x.OrderType == OrderType.Buy && x.Quantity * x.Rate > 2 && x.Rate % 0.00000010 > 0.000000001 && x.Rate % 0.00000005 > 0.000000001);
                IEnumerable<Order> Sells = re.Value.Where(x => x.OrderType == OrderType.Sell && x.Quantity * x.Rate > 2 && x.Rate % 0.00000010 > 0.000000001 && x.Rate % 0.00000005 > 0.000000001);
                logger.Write("--------------------");
                logger.Write($"{marketSummary.MarketName} - {Math.Round(marketSummary.BaseVolume)} -------- Buy ------------");
                if (Buies.Any())
                {
                    mes.Buies = Buies.OrderBy(x=>x.Rate);
                    foreach (Order order in Buies)
                    {
                        if (order.Rate % 0.00000010 > 0.000000001 && order.Rate % 0.00000005 > 0.000000001)
                        {
                            
                            logger.Write(
                                $"{marketSummary.Last}- {order.Quantity * order.Rate} - {order.Rate:0.#########}");
                        }
                    }

                    if (!Sells.Any())
                    {
                        logger.Write();
                    }
                    result.Add(mes);
                }

                if (Buies.Any() && Sells.Any())
                {
                    mes.Sells = Sells.OrderBy(x=>x.Rate);
                    logger.Write($"{marketSummary.MarketName} -------- Sell ------------");
                    foreach (Order order in Sells)
                    {
                        if (order.Rate % 0.00000010 > 0.000000001 && order.Rate % 0.00000005 > 0.000000001)
                        {

                            logger.Write(
                                $"{order.Quantity * order.Rate} - {order.Rate:0.#########}");
                        }
                    }

                    logger.Write();

                }

                //IEnumerable<Order> Buies1 = re.Value.Where(x => x.OrderType == OrderType.Buy).Where(x=>x.Rate * x.Quantity > 1.5).OrderByDescending(x=>x.Rate).Take(5);
                //IEnumerable<Order> Sells1 = re.Value.Where(x => x.OrderType == OrderType.Sell).Where(x => x.Rate * x.Quantity > 1.5).OrderBy(x => x.Rate).Take(5);

                //logger.Write("-------------- Top 5 ----------------------------");
                //logger.Write($"-------- Buy ------------");
                //foreach (Order order in Buies1)
                //{
                   

                //        logger.Write(
                //            $"{marketSummary.Last*100000000}- {order.Quantity * order.Rate} - {order.Rate * 100000000}");
                    
                //}
                //logger.Write($"{marketSummary.MarketName} -------- Sell ------------");
                //foreach (Order order in Sells1)
                //{
                    

                //        logger.Write(
                //            $"{order.Quantity * order.Rate} - {order.Rate * 100000000}");
                    
                //}
                //logger.Write("--------------------");
                //logger.Write();
            }

            foreach (Message message in result.OrderBy(x=>(x.Last - x.Buies.LastOrDefault().Rate)/ x.Last))
            {
                logger2.Write($"[{message.MarketName}] - [{message.BaseVolume}] - [{message.Last* 100000000}]----------");
                foreach (Order messageBuy in message.Buies)
                {
                    logger2.Write(
                                $"[Buy ]-[{messageBuy.Rate * 100000000}]-{messageBuy.Quantity * messageBuy.Rate}");
                }

                logger2.Write($"[Curr]-[{message.Last * 100000000}]");
                if (message.Sells != null)
                {
                    foreach (Order messageSell in message.Sells)
                    {
                        logger2.Write(
                            $"[Sell]-[{messageSell.Rate * 100000000}]-{messageSell.Quantity * messageSell.Rate}");
                    }
                }
                logger2.Write();
            }
            logger2.Close();

            logger.Close();

           

          
            Console.WriteLine("OK. Press any key.");
            Console.ReadKey();

            //markets[0].
            //List<Order> orders = objectManager.GetOrders("BTC-LTC");
            //int i = 0;

            //connection.GetAllCurrencies();
            //BittrexService bs = new BittrexService();
            //bs.Go();
            // Адрес ресурса, к которому выполняется запрос
            //Console.WriteLine("Введите значение ВЫШЕ которого будет сигнал");
            //string readLine = Console.ReadLine();
            //double max;
            //try
            //{
            //    max = double.Parse(readLine.Replace(".", ","));
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Произошла ошибка. Число не целое. Перезапустите программу");
            //   return;
            //}

            //Console.WriteLine("Введите значение НИЖЕ которого будет сигнал");
            // readLine = Console.ReadLine();
            //double min;
            //try
            //{
            //    min = double.Parse(readLine.Replace(".", ","));
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Произошла ошибка. Число не целое. Перезапустите программу");
            //    return;
            //}
            //string url = "https://www.fxclub.org/quotes/quotes.json?_=1496333215184";

            //string url2 =
            //    "https://gaterest.fxclub.com/Real/RestApi/Quotes/CurrentQuotes?version={0}&symbols=Litecoin";
            //   // "https://gaterest.fxclub.com/Real/RestApi/Quotes/CurrentQuotes?version=1496335643&symbols=Bitcoin%2CLitecoin%2CYM%2CEURUSD%2CAUDUSD%2CHG%2CPA%2CPL%2CXAGUSD%2CXAUUSD%2CGBPUSD%2CUSDJPY%2CUSDCAD%2CEURJPY%2CFDAX%2CBRN%2CAUDCAD%2CAUDCHF%2CAUDJPY%2CAUDNZD%2CCADCHF%2CCADJPY%2CCHFJPY%2CCHFSGD%2CEURAUD%2CEURCAD%2CEURCHF%2CEURGBP%2CEURMXN%2CEURNOK%2CEURNZD%2CEURRUB%2CEURSEK%2CEURSGD%2CGBPAUD%2CGBPCAD%2CGBPCHF%2CGBPJPY%2CGBPNZD%2CGBPSEK%2CNOKJPY%2CNZDCAD%2CNZDCHF%2CNZDJPY%2CNZDUSD%2CSGDJPY%2CUSDCHF%2CUSDDKK%2CUSDMXN%2CUSDNOK%2CUSDRUB%2CUSDSEK%2CUSDSGD%2CAA%2CAAPL%2CAXP%2CBA%2CBAC%2CC%2CCAT%2CCHL%2CCSCO%2CDD%2CDIS%2CF%2CFB%2CGE%2CGOOGL%2CHD%2CHOG%2CHPQ%2CIBM%2CINTC%2CJNJ%2CJPM%2CKO%2CMA%2CMCD%2CMSFT%2CNKE%2CPBR%2CPFE%2CPG%2CPM%2CPTR%2CSBUX%2CT%2CTRV%2CTSLA%2CUNH%2CV%2CVALE%2CVOD%2CVZ%2CXOM%2CYHOO%2CYNDX%2CES%2CFCE%2CFESX%2CFTI%2CZ%2CHSI%2CNKD%2CNQ%2CTF%2CCL%2CHO%2CNG%2CWT%2CRACE%2CBABA%2CEL%2CETH%2CJWN%2CKORS%2CLUX%2CPVH%2CRL%2CTIF%2CVFC%2CWSM%2CIDCB%2CLNVG%2CTCTZ%2CTA25%2CUSDCLP%2CCHILE%2CBSAC%2CEOC%2CSQM%2CADBE%2CAMZN%2CBIDU%2CCRM%2CEBAY%2CGS%2CNVDA%2CORCL%2CTM%2CTRIP%2CTWTR%2CADS%2CBMW%2CDBK%2CVOW%2CNINTENDO_US%2CNINTENDO_JP%2CCORN%2CWHEAT%2CSOYBEAN%2CCOFFEE%2CSUGAR%2CCOCOA%2CBAYN%2CCELG%2CDAI%2CENI%2CIBX%2CITX%2CMIB%2CWFC%2CXU%2CSNAP%2CRussia50%2CSberbank%2CLukoil%2CGazprom";
            ////var webClient1 = new WebClient();
            ////string downloadString = webClient1.DownloadString(url2);
            ////webClient1.Dispose();
            //long time = 14963334701178;

            //// Создаём объект WebClient
            //using (var webClient = new WebClient())
            //{
            //    while (true)
            //    {
            //        DateTime dateTime = DateTime.Now;
            //        TimeSpan timeSpan = dateTime.AddHours(-4) - new DateTime(1970, 01, 01);
            //        time += 1000;
            //        // Выполняем запрос по адресу и получаем ответ в виде строки
            //        long timeSpanTotalMilliseconds = (long)timeSpan.TotalMilliseconds/1000;
            //        string response = webClient.DownloadString(string.Format(url2, timeSpanTotalMilliseconds));


            //        string str = response.Substring(64, 6);
            //       double value = double.Parse(str.Replace(".", ","));

            //        Console.WriteLine(value);
            //        if (value < min || value > max)
            //            Console.Beep(5000, 500);

            //        Thread.Sleep(1000);

            //    }


            //}*/
        }
    }
}
