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
    public class UpdateAuctionItemCommandHandlerTests
    {
        private const string AuctionItemName = "boots";
        private readonly InMemoryEventStore _eventStore;
        private readonly AuctionId _auctionId;
        private readonly UpdateAuctionItemCommandHandler _handler;

        public UpdateAuctionItemCommandHandlerTests()
        {
            var auction = new AuctionAggregateFactory().Create("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem(AuctionItemName, "bill"));

            _eventStore = new InMemoryEventStore();
            _eventStore.Commit(auction.Id, auction.Changes);

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

            Assert.Equal(3, _eventStore.GetEventsById(_auctionId).Length);

            var updateEvent = (AuctionItemUpdatedEvent) _eventStore.GetEventsById(_auctionId).Last();
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
    }
}