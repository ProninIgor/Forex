using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class WebManager
    {
        private Dictionary<string, string> headers = new Dictionary<string, string>();

        public void AddHeader(string header, string value)
        {
            this.headers.Add(header, value);
        }

        public string GetStringResponse(string uri)
        {
            using (var webClient = new WebClient())
            {
                foreach (KeyValuePair<string, string> pair in this.headers)
                {
                    webClient.Headers.Add(pair.Key, pair.Value);
                }

                webClient.UseDefaultCredentials = true;
                string response = webClient.DownloadString(uri);
                return response;
            }
        }
    }
}
