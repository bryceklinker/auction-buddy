using System;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class UpdateAuctionCommandHandlerTests : IAsyncLifetime
    {
        private InMemoryEventStore _eventStore;
        private Core.Auctions.Auction _auction;
        private UpdateAuctionCommandHandler _handler;

        public async Task InitializeAsync()
        {
            _auction = new Core.Auctions.Auction("one", DateTimeOffset.UtcNow);
            
            _eventStore = new InMemoryEventStore();
            await _auction.CommitAsync(_eventStore);

            _handler = new UpdateAuctionCommandHandler(_eventStore);
        }

        [Fact]
        public async Task WhenUpdateAuctionCommandIsHandledThenReturnsSuccessfulResult()
        {
            var command = new UpdateAuctionCommand(_auction.Id, "something", DateTimeOffset.UtcNow);
            
            var result = await _handler.HandleAsync(command);

            Assert.True(result.WasSuccessful);
        }

        [Fact]
        public async Task WhenUpdateAuctionCommandIsHandledThenAuctionIsUpdated()
        {
            var auctionDate = DateTimeOffset.UtcNow;
            var command = new UpdateAuctionCommand(_auction.Id, "something", auctionDate);

            await _handler.HandleAsync(command);

            Assert.Equal(2, _eventStore.GetEventsById(_auction.Id).Length);

            var @event = (AuctionUpdatedEvent) _eventStore.GetEventsById(_auction.Id).Last();
            Assert.Equal("something", @event.Name);
            Assert.Equal(auctionDate, @event.AuctionDate);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}