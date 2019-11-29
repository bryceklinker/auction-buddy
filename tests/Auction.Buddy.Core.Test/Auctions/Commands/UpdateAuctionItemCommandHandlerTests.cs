using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class UpdateAuctionItemCommandHandlerTests : IAsyncLifetime
    {
        private const string AuctionItemName = "boots";
        private InMemoryEventStore _eventStore;
        private AuctionId _auctionId;
        private UpdateAuctionItemCommandHandler _handler;

        public async Task InitializeAsync()
        {
            var auction = new Core.Auctions.Auction("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem(AuctionItemName, "bill"));

            _eventStore = new InMemoryEventStore();
            await auction.CommitAsync(_eventStore);

            _auctionId = auction.Id;
            _handler = new UpdateAuctionItemCommandHandler(_eventStore);
        }

        [Fact]
        public async Task WhenUpdateAuctionItemCommandIsHandledThenReturnsSuccessfulResult()
        {
            var result = await _handler.HandleAsync(new UpdateAuctionItemCommand(_auctionId, AuctionItemName));

            Assert.True(result.WasSuccessful);
        }

        [Fact]
        public async Task WhenUpdateAuctionItemCommandIsHandledThenAuctionItemIsUpdated()
        {
            var command = new UpdateAuctionItemCommand(
                _auctionId, 
                AuctionItemName, 
                "name", 
                "jack",
                "this description",
                43
            );
            await _handler.HandleAsync(command);

            var updateEvent = (AuctionItemUpdatedEvent) _eventStore.GetLastEvent(_auctionId);
            Assert.Equal("name", updateEvent.NewName);
            Assert.Equal("jack", updateEvent.NewDonor);
            Assert.Equal("this description", updateEvent.NewDescription);
            Assert.Equal(43, updateEvent.NewQuantity);
        }

        [Fact]
        public async Task WhenUpdateAuctionItemCommandIsInvalidThenReturnsFailedResult()
        {
            var command = new UpdateAuctionItemCommand(_auctionId, null);
            var result = await _handler.HandleAsync(command);

            Assert.False(result.WasSuccessful);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}