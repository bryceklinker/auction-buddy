using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionCreatedEventHandler : DomainEventHandler<AuctionCreatedEvent, AuctionId>
    {
        private readonly ReadStore _readStore;

        public AuctionCreatedEventHandler(ReadStore readStore)
        {
            _readStore = readStore;
        }

        public async Task HandleAsync(AuctionCreatedEvent @event)
        {
            _readStore.Add(new AuctionReadModel
            {
                Id = @event.AggregateId,
                Name = @event.Name,
                AuctionDate = @event.AuctionDate
            });
            await _readStore.SaveAsync();
        }
    }
}