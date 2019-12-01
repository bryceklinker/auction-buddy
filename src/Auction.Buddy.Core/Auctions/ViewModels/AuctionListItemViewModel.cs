using System;

namespace Auction.Buddy.Core.Auctions.ViewModels
{
    public class AuctionListItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset AuctionDate { get; set; }
        public int ItemCount { get; set; }
    }
}