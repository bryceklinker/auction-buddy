using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Test.Support.Gateways;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class AddAuctionItemCommandHandlerTests
    {
        private readonly InMemoryAggregateGateway<Core.Auctions.Auction, AuctionId> _gateway;
        private readonly Core.Auctions.Auction _auction;
        private readonly AddAuctionItemCommandHandler _handler;

        public AddAuctionItemCommandHandlerTests()
        {
            _auction = new AuctionAggregateFactory().Create("one", DateTimeOffset.UtcNow);
            _gateway = new InMemoryAggregateGateway<Core.Auctions.Auction, AuctionId>();
            _gateway.Add(_auction);
            
            _handler = new AddAuctionItemCommandHandler(_gateway);
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

            var aggregate = await _gateway.FindByIdAsync(_auction.Id);
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