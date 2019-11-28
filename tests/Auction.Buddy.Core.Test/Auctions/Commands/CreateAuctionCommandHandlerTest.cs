using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class CreateAuctionCommandHandlerTest
    {
        private readonly InMemoryEventStore _eventStore;
        private readonly CreateAuctionCommandHandler _handler;

        public CreateAuctionCommandHandlerTest()
        {
            _eventStore = new InMemoryEventStore();
            
            _handler = new CreateAuctionCommandHandler(_eventStore);
        }

        [Fact]
        public async Task WhenCreateAuctionCommandIsHandledThenAuctionIsSaved()
        {
            var auctionDate = DateTimeOffset.UtcNow;
            var result = await _handler.HandleAsync(new CreateAuctionCommand("idk", auctionDate));

            Assert.Single(_eventStore.GetEventsById(result.Result));
        }

        [Fact]
        public async Task WhenCreateAuctionCommandIsHandledThenAuctionIsPopulatedFromCommand()
        {
            var auctionDate = DateTimeOffset.UtcNow;
            var result = await _handler.HandleAsync(new CreateAuctionCommand("something", auctionDate));

            var createEvent = (AuctionCreatedEvent) _eventStore.GetEventsById(result.Result)[0];
            Assert.Equal("something", createEvent.Name);
            Assert.Equal(auctionDate, createEvent.AuctionDate);
        }

        [Fact]
        public async Task WhenCreateAuctionCommandIsHandledThenReturnsSuccessfulResult()
        {
            var result = await _handler.HandleAsync(new CreateAuctionCommand("idk", DateTimeOffset.UtcNow));
            
            Assert.True(result.WasSuccessful);
        }

        [Fact]
        public async Task WhenCreateAuctionCommandIsInvalidThenReturnsFailedResult()
        {
            var result = await _handler.HandleAsync(new CreateAuctionCommand(null, DateTimeOffset.UtcNow));

            Assert.False(result.WasSuccessful);
        }
    }
}