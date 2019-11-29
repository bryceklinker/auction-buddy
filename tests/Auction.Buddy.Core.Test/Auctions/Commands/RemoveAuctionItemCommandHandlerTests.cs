using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class RemoveAuctionItemCommandHandlerTests : IAsyncLifetime
    {
        private const string AuctionItemName = "three";
        private InMemoryEventStore _eventStore;
        private AuctionId _auctionId;
        private RemoveAuctionItemCommandHandler _handler;

        public async Task InitializeAsync()
        {
            var auction = new Core.Auctions.Auction("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem(AuctionItemName, "four"));
            
            _eventStore = new InMemoryEventStore();
            await auction.CommitAsync(_eventStore);

            _auctionId = auction.Id;
            _handler = new RemoveAuctionItemCommandHandler(_eventStore);
        }

        [Fact]
        public async Task WhenRemoveAuctionItemCommandIsHandledThenReturnsSuccessfulResult()
        {
            var result = await _handler.HandleAsync(new RemoveAuctionItemCommand(_auctionId, AuctionItemName));

            Assert.True(result.WasSuccessful);
        }

        [Fact]
        public async Task WhenRemoveAuctionItemCommandIsHandledThenAuctionItemIsRemovedFromAuction()
        {
            await _handler.HandleAsync(new RemoveAuctionItemCommand(_auctionId, AuctionItemName));
            
            var removeEvent = (AuctionItemRemovedEvent) _eventStore.GetLastEvent(_auctionId);
            Assert.Equal(AuctionItemName, removeEvent.Name);
        }

        [Fact]
        public async Task WhenRemoveAuctionItemCommandIsInvalidThenReturnsFailedResult()
        {
            var result = await _handler.HandleAsync(new RemoveAuctionItemCommand(_auctionId, null));

            Assert.False(result.WasSuccessful);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}