using System;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionCreatedEvent : DomainEventBase<AuctionId>
    {
        public string Name { get; }
        public DateTimeOffset AuctionDate { get; }
        
        public AuctionCreatedEvent(AuctionId aggregateId, string name, DateTimeOffset auctionDate, DateTimeOffset? timestamp = null) 
            : base(aggregateId, timestamp)
        {
            Name = name;
            AuctionDate = auctionDate;
        }
    }
}