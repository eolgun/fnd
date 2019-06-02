using System;
using Funda.Service;

namespace Funda.RealEstateBillBoard
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var billBoard = new RealEstateBillBoard(new PartnerService(new MyHttpClient()), new AddressBuilder(),
                new Settings(3500));
            billBoard.GetTop10RealEstate(false).Wait();

            Console.WriteLine("Press any key to see the top 10 real estate agency selling properties with garden. ");
            Console.ReadLine();

            billBoard.GetTop10RealEstate(true).Wait();

            Console.ReadLine();

        }
    }
}
