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
    public class RemoveAuctionItemCommandHandlerTests
    {
        private const string AuctionItemName = "three";
        private readonly InMemoryEventStore _eventStore;
        private readonly AuctionId _auctionId;
        private readonly RemoveAuctionItemCommandHandler _handler;

        public RemoveAuctionItemCommandHandlerTests()
        {
            var auction = new AuctionAggregateFactory().Create("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem(AuctionItemName, "four"));
            
            _eventStore = new InMemoryEventStore();
            _eventStore.Commit(auction.Id, auction.Changes);

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
            
            Assert.Equal(3, _eventStore.GetEventsById(_auctionId).Length);

            var removeEvent = (AuctionItemRemovedEvent) _eventStore.GetEventsById(_auctionId).Last();
            Assert.Equal(AuctionItemName, removeEvent.Name);
        }

        [Fact]
        public async Task WhenRemoveAuctionItemCommandIsInvalidThenReturnsFailedResult()
        {
            var result = await _handler.HandleAsync(new RemoveAuctionItemCommand(_auctionId, null));

            Assert.False(result.WasSuccessful);
        }
    }
}