using System;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Test.Support.Gateways;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class UpdateAuctionItemCommandHandlerTests
    {
        private const string AuctionItemName = "boots";
        private readonly InMemoryAggregateGateway<Core.Auctions.Auction, AuctionId> _gateway;
        private readonly Core.Auctions.Auction _auction;
        private readonly UpdateAuctionItemCommandHandler _handler;

        public UpdateAuctionItemCommandHandlerTests()
        {
            _auction = new AuctionAggregateFactory().Create("one", DateTimeOffset.UtcNow);
            _auction.AddAuctionItem(new AuctionItem(AuctionItemName, "bill"));
            
            _gateway = new InMemoryAggregateGateway<Core.Auctions.Auction, AuctionId>();
            _gateway.Add(_auction);
            
            _handler = new UpdateAuctionItemCommandHandler(_gateway);
        }

        [Fact]
        public async Task WhenUpdateAuctionItemCommandIsHandledThenReturnsSuccessfulResult()
        {
            var result = await _handler.HandleAsync(new UpdateAuctionItemCommand(_auction.Id, AuctionItemName));

            Assert.True(result.WasSuccessful);
        }

        [Fact]
        public async Task WhenUpdateAuctionItemCommandIsHandledThenAuctionItemIsUpdated()
        {
            var command = new UpdateAuctionItemCommand(
                _auction.Id, 
                AuctionItemName, 
                "name", 
                "jack",
                "this description",
                43
            );
            await _handler.HandleAsync(command);

            var auctionItem = _gateway.Aggregates[0].Items.First();
            Assert.Equal("name", auctionItem.Name);
            Assert.Equal("jack", auctionItem.Donor);
            Assert.Equal("this description", auctionItem.Description);
            Assert.Equal(43, auctionItem.Quantity);
        }

        [Fact]
        public async Task WhenUpdateAuctionItemCommandIsInvalidThenReturnsFailedResult()
        {
            var command = new UpdateAuctionItemCommand(_auction.Id, null);
            var result = await _handler.HandleAsync(command);

            Assert.False(result.WasSuccessful);
        }
    }
}