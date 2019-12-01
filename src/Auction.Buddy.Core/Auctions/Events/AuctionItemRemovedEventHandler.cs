using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Auctions.Events
{
    public class AuctionItemRemovedEventHandler : DomainEventHandler<AuctionItemRemovedEvent, AuctionId>
    {
        private readonly ReadStore _readStore;

        public AuctionItemRemovedEventHandler(ReadStore readStore)
        {
            _readStore = readStore;
        }

        public async Task HandleAsync(AuctionItemRemovedEvent @event)
        {
            var item = _readStore.GetAuctionItem(@event.AggregateId, @event.Name);
            _readStore.Remove(item);
            await _readStore.SaveAsync();
        }
    }
}