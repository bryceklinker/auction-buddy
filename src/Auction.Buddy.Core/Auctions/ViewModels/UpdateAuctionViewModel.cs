using System;

namespace Auction.Buddy.Core.Auctions.ViewModels
{
    public class UpdateAuctionViewModel
    {
        public string Name { get; set; }
        public DateTimeOffset? AuctionDate { get; set; }
    }
}