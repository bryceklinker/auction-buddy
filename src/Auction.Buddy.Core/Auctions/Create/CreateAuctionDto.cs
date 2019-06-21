using System;

namespace Harvest.Home.Core.Auctions.Create
{
    public class CreateAuctionDto
    {
        public string Name { get; set; }
        public DateTimeOffset AuctionDate { get; set; }
    }
}