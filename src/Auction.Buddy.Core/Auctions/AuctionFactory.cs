using System;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Auctions
{
    public interface AuctionFactory
    {
        Auction Create(string name, DateTimeOffset auctionDate);
    }

    public class AuctionAggregateFactory : AuctionFactory
    {
        public Auction Create(string name, DateTimeOffset auctionDate)
        {
            var id = new AuctionId();
            return new Auction(id, name, auctionDate);
        }
    }
}