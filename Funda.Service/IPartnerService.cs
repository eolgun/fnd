using System.Collections.Generic;
using System.Threading.Tasks;
using Funda.Domain;

namespace Funda.Service
{
    public interface IPartnerService
    {
        Task<ServiceResponse> Map(string postUrl);
        List<RealEstateAgent> Reduce(List<RealEstateAgent> realEstateByCount, Dictionary<string, int> dictionary);
    }
}