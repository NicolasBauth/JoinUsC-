using System;
using System.Net.Http;
using System.Net.Http.Headers;

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
                    _client.BaseAddress = new Uri("http://apijoynus.azurewebsites.net");
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(" x-www-form-urlencoded"));
                }
                return _client;
            }
        }
    }
}
