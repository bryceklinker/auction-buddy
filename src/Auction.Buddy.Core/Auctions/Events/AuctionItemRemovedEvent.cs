using System;
using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionItemRemovedEvent : DomainEventBase<AuctionId>
    {
        public string Name { get; }
        
        public AuctionItemRemovedEvent(AuctionId aggregateId, string name, DateTimeOffset? timestamp = null) 
            : base(aggregateId, timestamp)
        {
            Name = name;
        }
    }
}