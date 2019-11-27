using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Test.Support.Gateways;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Commands
{
    public class RemoveAuctionItemCommandHandlerTests
    {
        private const string AuctionItemName = "three";
        private readonly InMemoryAggregateGateway<Core.Auctions.Auction, AuctionId> _gateway;
        private readonly Core.Auctions.Auction _auction;
        private readonly RemoveAuctionItemCommandHandler _handler;

        public RemoveAuctionItemCommandHandlerTests()
        {
            _auction = new AuctionAggregateFactory().Create("one", DateTimeOffset.UtcNow);
            _auction.AddAuctionItem(new AuctionItem(AuctionItemName, "four"));
            
            _gateway = new InMemoryAggregateGateway<Core.Auctions.Auction, AuctionId>();
            _gateway.Add(_auction);
            
            _handler = new RemoveAuctionItemCommandHandler(_gateway);
        }

        [Fact]
        public async Task WhenRemoveAuctionItemCommandIsHandledThenReturnsSuccessfulResult()
        {
            var result = await _handler.HandleAsync(new RemoveAuctionItemCommand(_auction.Id, AuctionItemName));

            Assert.True(result.WasSuccessful);
        }

        [Fact]
        public async Task WhenRemoveAuctionItemCommandIsHandledThenAuctionItemIsRemovedFromAuction()
        {
            await _handler.HandleAsync(new RemoveAuctionItemCommand(_auction.Id, AuctionItemName));

            Assert.Empty(_gateway.Aggregates[0].Items);
        }

        [Fact]
        public async Task WhenRemoveAuctionItemCommandIsInvalidThenReturnsFailedResult()
        {
            var result = await _handler.HandleAsync(new RemoveAuctionItemCommand(null, AuctionItemName));

            Assert.False(result.WasSuccessful);
        }
    }
}