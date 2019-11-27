using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Test.Support.Gateways;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class CreateAuctionCommandHandlerTest
    {
        private readonly InMemoryAggregateGateway<Core.Auctions.Auction, AuctionId> _gateway;
        private readonly CreateAuctionCommandHandler _handler;

        public CreateAuctionCommandHandlerTest()
        {
            _gateway = new InMemoryAggregateGateway<Core.Auctions.Auction, AuctionId>();
            
            _handler = new CreateAuctionCommandHandler(_gateway);
        }

        [Fact]
        public async Task WhenCreateAuctionCommandIsHandledThenAuctionIsSaved()
        {
            await _handler.HandleAsync(new CreateAuctionCommand("idk", DateTimeOffset.UtcNow));

            Assert.Single(_gateway.Aggregates);
        }

        [Fact]
        public async Task WhenCreateAuctionCommandIsHandledThenAuctionIsPopulatedFromCommand()
        {
            var auctionDate = DateTimeOffset.UtcNow;
            await _handler.HandleAsync(new CreateAuctionCommand("something", auctionDate));

            var addedAuction = _gateway.Aggregates[0];
            Assert.Equal("something", addedAuction.Name);
            Assert.Equal(auctionDate, addedAuction.AuctionDate);
        }

        [Fact]
        public async Task WhenCreateAuctionCommandIsHandledThenReturnsSuccessfulResult()
        {
            var result = await _handler.HandleAsync(new CreateAuctionCommand("idk", DateTimeOffset.UtcNow));
            
            Assert.True(result.WasSuccessful);
        }

        [Fact]
        public async Task WhenCreateAuctionCommandIsHandledThenReturnsAuctionId()
        {
            var result = await _handler.HandleAsync(new CreateAuctionCommand("one", DateTimeOffset.UtcNow));

            var auction = _gateway.Aggregates[0];
            Assert.Equal(auction.Id, result.Result);
        }

        [Fact]
        public async Task WhenCreateAuctionCommandIsInvalidThenReturnsFailedResult()
        {
            var result = await _handler.HandleAsync(new CreateAuctionCommand(null, DateTimeOffset.UtcNow));

            Assert.False(result.WasSuccessful);
        }
    }
}