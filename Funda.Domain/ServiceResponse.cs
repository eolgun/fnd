using System.Collections.Generic;
using System.Net;

namespace Funda.Service
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string PostUrl { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, int> Dictionary { get; set; }
    }
}