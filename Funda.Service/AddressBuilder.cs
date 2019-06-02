namespace Funda.Service
{
    public class AddressBuilder
    {
        public string GetUrl(int pageId, int pageSize, string location, string filter)
        {
            var baseAddress =
                $"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/{location}/{filter}/&page={pageId}&pagesize={pageSize}";

            return baseAddress;
        }

        public string GetUrl(int pageId, int pageSize, string location)
        {
            var baseAddress =
                $"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/{location}/&page={pageId}&pagesize={pageSize}";

            return baseAddress;
        }
    }
}