using Auction.Buddy.Core.Common.Events;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionItemUpdatedEvent : DomainEventBase<AuctionId>
    {
        public string OldName { get; }
        public string NewName { get; }
        public string NewDonor { get; }
        public string NewDescription { get; }
        public int? NewQuantity { get; }
        
        public AuctionItemUpdatedEvent(AuctionId aggregateId, string oldName, string newName = null, string newDonor = null, string newDescription = null, int? newQuantity = null) 
            : base(aggregateId)
        {
            OldName = oldName;
            NewName = newName;
            NewDonor = newDonor;
            NewDescription = newDescription;
            NewQuantity = newQuantity;
        }
    }
}