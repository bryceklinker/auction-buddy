using System;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionCreatedEvent : DomainEventBase<AuctionId>
    {
        public string Name { get; }
        public DateTimeOffset AuctionDate { get; }
        
        public AuctionCreatedEvent(AuctionId aggregateId, string name, DateTimeOffset auctionDate) 
            : base(aggregateId)
        {
            Name = name;
            AuctionDate = auctionDate;
        }
    }
}