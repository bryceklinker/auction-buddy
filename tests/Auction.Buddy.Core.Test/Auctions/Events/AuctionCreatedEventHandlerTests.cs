using System;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Events
{
    public class AuctionCreatedEventHandlerTests
    {
        private readonly InMemoryReadStore _readStore;
        private readonly AuctionCreatedEventHandler _handler;

        public AuctionCreatedEventHandlerTests()
        {
            _readStore = new InMemoryReadStore();
            _handler = new AuctionCreatedEventHandler(_readStore);
        }

        [Fact]
        public async Task WhenAuctionCreatedThenAuctionIsAddedToReadStore()
        {
            var @event = new AuctionCreatedEvent(new AuctionId(), "one", DateTimeOffset.UtcNow);

            await _handler.HandleAsync(@event);

            Assert.Single(_readStore.GetAll<AuctionReadModel>());
        }

        [Fact]
        public async Task WhenAuctionCreatedThenAuctionIsPopulatedFromCreatedEvent()
        {
            var @event = new AuctionCreatedEvent(new AuctionId(), "one", DateTimeOffset.UtcNow);

            await _handler.HandleAsync(@event);

            var model = _readStore.GetAll<AuctionReadModel>().Single();
            Assert.Equal(@event.AggregateId.ToString(), model.Id);
            Assert.Equal("one", model.Name);
            Assert.Equal(@event.AuctionDate, model.AuctionDate);
        }
    }
}