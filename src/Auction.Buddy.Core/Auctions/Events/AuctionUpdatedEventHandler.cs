using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionUpdatedEventHandler : DomainEventHandler<AuctionUpdatedEvent, AuctionId>
    {
        private readonly ReadStore _readStore;

        public AuctionUpdatedEventHandler(ReadStore readStore)
        {
            _readStore = readStore;
        }

        public async Task HandleAsync(AuctionUpdatedEvent @event)
        {
            var auction = await _readStore.GetAuctionAsync(@event.AggregateId);
            auction.Name = @event.Name ?? auction.Name;
            auction.AuctionDate = @event.AuctionDate ?? auction.AuctionDate;
            
            await _readStore.SaveAsync();
        }
    }
}