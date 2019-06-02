using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Funda.Domain;
using Funda.Service;

namespace Funda.RealEstateBillBoard
{
    public class RealEstateBillBoard
    {
        private readonly AddressBuilder _addressBuilder;
        private readonly IPartnerService _partnerService;
        private readonly Settings _settings;
        
        public RealEstateBillBoard(IPartnerService partnerService, AddressBuilder addressBuilder, Settings settings)
        {
            _partnerService = partnerService;
            _addressBuilder = addressBuilder;
            _settings = settings;
        }

        public async Task<IEnumerable<RealEstateAgent>> GetTop10RealEstate(bool withGarden)
        {
            var maxPageSize = _settings.MaxPageSize;
            var maxPropertyCount = _settings.MaxPropertyCount;
            var pageCount = (int)Math.Ceiling(maxPropertyCount * 1.0 / maxPageSize);

            var tasks = new List<Task<ServiceResponse>>();
            for (var i = 1; i <= pageCount; i++)
            {
                string postUrl;
                if (withGarden)
                {
                    postUrl = _addressBuilder.GetUrl(i, 25, "amsterdam", "tuin");
                }
                else
                {
                    postUrl = _addressBuilder.GetUrl(i, 25, "amsterdam");
                }

                tasks.Add(_partnerService.Map(postUrl));
                Console.WriteLine($"Request send to : {postUrl} ");

                if (i % 3 == 0)
                {
                    //3 task in max 2 sc.
                    await Task.Delay(2000);
                }
            }

            var billboard = new List<RealEstateAgent>();
            while (tasks.Any())
            {
                var finished = await Task.WhenAny(tasks);
                tasks.Remove(finished);
                var finishedCall = await finished;
                Console.WriteLine($"Response : {finishedCall.PostUrl} ");
                if (finishedCall.Success)
                {
                    billboard = _partnerService.Reduce(billboard, finishedCall.Dictionary);
                }
                else
                {
                    Console.WriteLine($"Warning : Not reduced, will be enqueued {finishedCall.PostUrl} ");
                    Thread.Sleep(_settings.RetryInMiliSeconds);
                    tasks.Add(_partnerService.Map(finishedCall.PostUrl));
                }
            }

            var countOfProperties = billboard.Sum(m => m.Count);
            Console.WriteLine("Total Count of property: " + countOfProperties);

            var topN = billboard.OrderByDescending(m => m.Count).Take(_settings.ItemCountInBillBoard);
            foreach (var realEstateAgent in topN)
            {
                Console.WriteLine($"{realEstateAgent.Name} : {realEstateAgent.Count}");
            }

            return topN;
        }
    }
}