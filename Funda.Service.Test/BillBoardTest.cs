using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Funda.Service.Test
{
    public class BillBoardTest
    {
        public BillBoardTest()
        {
            string line;
            using (var reader = new StreamReader("ServiceResponse.txt"))
            {
                line = reader.ReadToEnd();
            }

            _client = new Mock<IHttpClient>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                RequestMessage = new HttpRequestMessage(),
                Content = new StringContent(line)
            };
            
            _client.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));
            _chart = new RealEstateBillBoard.RealEstateBillBoard(new PartnerService(_client.Object),
                new AddressBuilder(), new Settings(25));
        }

        private readonly Mock<IHttpClient> _client;

        private readonly RealEstateBillBoard.RealEstateBillBoard _chart;

        [Fact(DisplayName = "GetTop10RealEstate count should be .")]
        public async Task Test()
        {
            var list = await _chart.GetTop10RealEstate(false);
            Assert.NotNull(list.FirstOrDefault(m => m.Name == "Voogd Makelaardij"));
        }
    }
}