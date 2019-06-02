using Xunit;

namespace Funda.Service.Test
{
    public class AddressBuilderTests
    {
        private readonly AddressBuilder _addressBuilder = new AddressBuilder();

        [Fact(DisplayName = "Get post Url - Amsterdam, With garden")]
        public void PostUrlForAmsterdamWithGarden()
        {
            var expected =
                "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=25";
            var postUrl = _addressBuilder.GetUrl(1, 25, "amsterdam", "tuin");
            Assert.Equal(postUrl, expected);
        }

        [Fact(DisplayName = "Get post Url - Amsterdam, Without filter")]
        public void PostUrlForAmsterdamWithoutGarden()
        {
            var expected =
                "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/&page=1&pagesize=25";
            var postUrl = _addressBuilder.GetUrl(1, 25, "amsterdam");
            Assert.Equal(postUrl, expected);
        }
    }
}