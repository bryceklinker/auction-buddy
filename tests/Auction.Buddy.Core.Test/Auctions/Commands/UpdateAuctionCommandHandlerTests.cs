using System;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class UpdateAuctionCommandHandlerTests
    {
        private readonly InMemoryEventStore _eventStore;
        private readonly Core.Auctions.Auction _auction;
        private readonly UpdateAuctionCommandHandler _handler;

        public UpdateAuctionCommandHandlerTests()
        {
            _auction = new AuctionAggregateFactory().Create("one", DateTimeOffset.UtcNow);
            
            _eventStore = new InMemoryEventStore();
            _eventStore.Commit(_auction.Id, _auction.Changes);

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
    }
}