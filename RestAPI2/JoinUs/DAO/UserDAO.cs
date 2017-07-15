using JoinUs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.DAO
{
    public static class UserDAO
    {
        public static async Task<User> AuthenticateUser(string requestedLogin, string requestedPassword)
        {
            HttpClient client = HttpClientSingleton.Client;
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("Username",requestedLogin),
                new KeyValuePair<string, string>("Password", requestedPassword)
            });
            var response = await client.PostAsync("token",formContent);
        }
    }
}
