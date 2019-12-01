using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionItemAddedEventHandler : DomainEventHandler<AuctionItemAddedEvent, AuctionId>
    {
        private readonly ReadStore _readStore;

        public AuctionItemAddedEventHandler(ReadStore readStore)
        {
            _readStore = readStore;
        }

        public async Task HandleAsync(AuctionItemAddedEvent @event)
        {
            _readStore.Add(new AuctionItemReadModel
            {
                AuctionId = @event.AggregateId,
                Name = @event.Item.Name,
                Description = @event.Item.Description,
                Donor = @event.Item.Donor,
                Quantity = @event.Item.Quantity
            });
            await _readStore.SaveAsync();
        }
    }
}