using System;
using System.Net.Http;
using System.Threading.Tasks;
using Funda.Api.RealEstate.DTOs;
using Funda.Api.RealEstate.Impl;
using RateLimiter;

namespace FundaApiTestTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            RealEstateResult result;
            var t = TimeLimiter.GetFromMaxCountByInterval(100, TimeSpan.FromMinutes(1))
            var client = new HttpClient()
            using (var requester = new FundaRealEstateRequester())
            {
                result = await requester.Request(
                    "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/tuin/&pagesize=575");
                /*var tasks = new List<Task>();
                for (int i = 0; i < 101; i++)
                {
                    tasks.Add(requester.Request(
                        "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/tuin/&pagesize=575"));
                }

                await Task.WhenAll(tasks);*/
                Console.WriteLine("Starting requests");
                Console.WriteLine(DateTime.Now);
                for (int i = 1; i < 103; i++)
                {
                    await requester.Request(
                        "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/?type=koop&zo=/amsterdam/tuin/&pagesize=575");
                    Console.WriteLine(i);
                }
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("Finished");
            }
            Console.WriteLine($"{result.Objects.Count}");
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
