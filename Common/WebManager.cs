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
        public string GetStringResponse(string uri)
        {
            using (var webClient = new WebClient())
            {
                webClient.UseDefaultCredentials = true;
                string response = webClient.DownloadString(uri);
                return response;
            }
        }
    }
}
