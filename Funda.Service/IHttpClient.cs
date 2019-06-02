using System.Net.Http;
using System.Threading.Tasks;

namespace Funda.Service
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}