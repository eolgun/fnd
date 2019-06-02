using System.Net.Http;
using System.Threading.Tasks;

namespace Funda.Service
{
    public class MyHttpClient : IHttpClient
    {
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                response = await client.GetAsync(url);
            }

            return response;
        }
    }
}