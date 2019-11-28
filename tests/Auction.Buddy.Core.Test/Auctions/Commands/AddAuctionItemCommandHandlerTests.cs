using System;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class AddAuctionItemCommandHandlerTests
    {
        private readonly InMemoryEventStore _eventStore;
        private readonly Core.Auctions.Auction _auction;
        private readonly AddAuctionItemCommandHandler _handler;

        public AddAuctionItemCommandHandlerTests()
        {
            _auction = new AuctionAggregateFactory().Create("one", DateTimeOffset.UtcNow);
            _eventStore = new InMemoryEventStore();
            _eventStore.Commit(_auction.Id, _auction.Changes.ToArray());
            
            _handler = new AddAuctionItemCommandHandler(_eventStore);
        }

        [Fact]
        public async Task WhenAddAuctionItemCommandIsHandledThenReturnsSuccessfulResult()
        {
            var auctionItem = new AuctionItem("one", "bob");
            var result = await _handler.HandleAsync(new AddAuctionItemCommand(_auction.Id, auctionItem));

            Assert.True(result.WasSuccessful);
        }

        [Fact]
        public async Task WhenAddAuctionItemCommandIsHandledThenAuctionItemIsAddedToAuction()
        {
            var auctionItem = new AuctionItem("one", "three");
            await _handler.HandleAsync(new AddAuctionItemCommand(_auction.Id, auctionItem));

            var aggregate = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(_auction.Id);
            Assert.Single(aggregate.Items);
        }

        [Fact]
        public async Task WhenAddAuctionItemCommandIsInvalidThenReturnsFailedResult()
        {
            var auctionItem = new AuctionItem(null, null);
            var result = await _handler.HandleAsync(new AddAuctionItemCommand(_auction.Id, auctionItem));

            Assert.False(result.WasSuccessful);
        }
    }
}