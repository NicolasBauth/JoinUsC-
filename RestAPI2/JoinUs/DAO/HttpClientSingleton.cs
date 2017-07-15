using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.DAO
{
    public class HttpClientSingleton
    {
        private static HttpClient _client;
        private HttpClientSingleton()
        {

        }
        public static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.BaseAddress = new Uri("http://joinusapiv3.azurewebsites.net/");
                }
                return _client;
            }
        }
    }
}
