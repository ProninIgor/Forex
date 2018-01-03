using System;
using System.IO;
using System.Net;
using System.Threading;
using Bittrex;
using Newtonsoft.Json;

namespace Forex
{
    class Program
    {
        static void Main(string[] args)
        {
            BittrexService bs = new BittrexService();
            bs.Go();
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
               

            //}
        }
    }
}
