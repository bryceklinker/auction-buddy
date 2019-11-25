using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionItemAddedEvent : DomainEventBase<AuctionId>
    {
        public AuctionItem Item { get; }

        public AuctionItemAddedEvent(AuctionId aggregateId, AuctionItem item) 
            : base(aggregateId)
        {
            Item = item;
        }
    }
}