using System;
using System.Collections.Generic;

namespace Auction.Buddy.Core.Auctions
{
    public class AuctionReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset AuctionDate { get; set; }
        public ICollection<AuctionItemReadModel> Items { get; set; }
    }
}