using System;

namespace Auction.Buddy.Core.Auctions.ViewModels
{
    public class CreateAuctionViewModel
    {
        public string Name { get; set; }
        public DateTimeOffset AuctionDate { get; set; }
    }
}