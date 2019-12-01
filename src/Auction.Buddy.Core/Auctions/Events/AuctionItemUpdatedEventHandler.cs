using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionItemUpdatedEventHandler : DomainEventHandler<AuctionItemUpdatedEvent, AuctionId>
    {
        private readonly ReadStore _readStore;

        public AuctionItemUpdatedEventHandler(ReadStore readStore)
        {
            _readStore = readStore;
        }

        public async Task HandleAsync(AuctionItemUpdatedEvent @event)
        {
            var item = await _readStore.GetAuctionItemAsync(@event.AggregateId, @event.OldName);

            item.Name = @event.NewName ?? item.Name;
            item.Description = @event.NewDescription ?? item.Description;
            item.Donor = @event.NewDonor ?? item.Donor;
            item.Quantity = @event.NewQuantity ?? item.Quantity;
            await _readStore.SaveAsync();
        }
    }
}