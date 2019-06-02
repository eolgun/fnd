using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funda.Domain;
using Newtonsoft.Json.Linq;

namespace Funda.Service
{
    public class PartnerService : IPartnerService
    {
        private readonly IHttpClient _httpClient;

        public PartnerService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse> Map(string postUrl)
        {
            var serviceResponse = new ServiceResponse {Dictionary = new Dictionary<string, int>()};
            try
            {
                var httpResponse = await _httpClient.GetAsync(postUrl);
                serviceResponse.StatusCode = httpResponse.StatusCode;
                serviceResponse.PostUrl = postUrl;

                if (httpResponse.IsSuccessStatusCode)
                {
                    serviceResponse.Success = true;
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    serviceResponse.Dictionary = Parse(json);
                }
                else
                {
                    serviceResponse.Success = false;
                    Console.WriteLine($"{httpResponse.ReasonPhrase} : {postUrl}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} : Post Url : {postUrl}");
            }

            return serviceResponse;
        }


        public List<RealEstateAgent> Reduce(List<RealEstateAgent> realEstateByCountList,
            Dictionary<string, int> dictionaryRealEstateAndCount)
        {
            foreach (var key in dictionaryRealEstateAndCount.Keys)
            {
                var current = realEstateByCountList.FirstOrDefault(m => m.Name == key);
                if (current == null)
                {
                    realEstateByCountList.Add(new RealEstateAgent
                    {
                        Name = key,
                        Count = dictionaryRealEstateAndCount[key]
                    });
                }
                else
                {
                    current.Count = current.Count + dictionaryRealEstateAndCount[key];
                }
            }

            return realEstateByCountList;
        }

        private Dictionary<string, int> Parse(string json)
        {
            var jobject = JObject.Parse(json);
            var objects = jobject.Property("Objects");
            var current = objects.First;

            var propertyCountByRealEstate = new Dictionary<string, int>();
            foreach (var child in current.Children())
            {
                var realEstName = child.Value<string>("MakelaarNaam");

                if (propertyCountByRealEstate.ContainsKey(realEstName))
                {
                    propertyCountByRealEstate[realEstName] = propertyCountByRealEstate[realEstName] + 1;
                }
                else
                {
                    propertyCountByRealEstate.Add(realEstName, 1);
                }
            }

            return propertyCountByRealEstate;
        }
    }
}