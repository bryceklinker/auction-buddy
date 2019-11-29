using System;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionUpdatedEvent : DomainEventBase<AuctionId>
    {
        public string Name { get; }
        public DateTimeOffset? AuctionDate { get; }
        
        public AuctionUpdatedEvent(AuctionId aggregateId, string name, in DateTimeOffset? auctionDate, DateTimeOffset? timestamp = null) 
            : base(aggregateId, timestamp)
        {
            Name = name;
            AuctionDate = auctionDate;
        }
    }
}