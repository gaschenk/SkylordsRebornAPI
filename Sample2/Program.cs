using System;
using System.Linq;
using Newtonsoft.Json;
using SkylordsRebornAPI;
using SkylordsRebornAPI.Auction;

namespace Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestBody requestBody = new RequestBody
            {
                Input = "",
                Min = 0,
                Max = 0,
            };
            Console.WriteLine($"Amount of auctions: {Instances.AuctionService.GetAmountOfAuctions(requestBody)}");
            var cardInfo = Instances.AuctionService.GetCardInfo(CardId.Batariel);
            var nameOfCard = Enum.GetName(typeof(CardId), CardId.Batariel);
            Console.WriteLine($"Card Info {nameOfCard}: \n{JsonConvert.SerializeObject(cardInfo)}");
            var auctionEntries = Instances.AuctionService.GetAuctionEntriesOfPage(1, 30, requestBody);
            var firstOrDefaultEntry = auctionEntries.FirstOrDefault();
            Console.WriteLine($"AuctionEntry: {JsonConvert.SerializeObject(firstOrDefaultEntry)}");
            if (firstOrDefaultEntry != null)
                Console.WriteLine(
                    $"Specific AuctionEntry: {JsonConvert.SerializeObject(Instances.AuctionService.GetAuctionEntryInfo((int) firstOrDefaultEntry.AuctionId))}");
        }
    }
}