using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Test.Support.Gateways;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Validation
{
    public class RemoveAuctionItemCommandValidatorTests
    {
        private const string ItemName = "item";
        private readonly AuctionId _existingAuctionId;
        private readonly RemoveAuctionItemCommandValidator _validator;

        public RemoveAuctionItemCommandValidatorTests()
        {
            var auction = new AuctionAggregateFactory().Create("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem(ItemName, "donor"));
            _existingAuctionId = auction.Id;
            
            var gateway = new InMemoryAggregateGateway<Core.Auctions.Auction, AuctionId>();
            gateway.Add(auction);
            
            _validator = new RemoveAuctionItemCommandValidator(gateway);
        }

        [Fact]
        public async Task WhenItemNameIsNullThenReturnsInvalid()
        {
            var result = await _validator.ValidateAsync(new RemoveAuctionItemCommand(_existingAuctionId, null));

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenItemNameIsEmptyThenReturnsInvalid()
        {
            var result = await _validator.ValidateAsync(new RemoveAuctionItemCommand(_existingAuctionId, string.Empty));

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenAuctionIdIsNullThenReturnsInvalid()
        {
            var result = await _validator.ValidateAsync(new RemoveAuctionItemCommand(null, ItemName));

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenAuctionIdDoesNotExistThenReturnsInvalid()
        {
            var result = await _validator.ValidateAsync(new RemoveAuctionItemCommand(new AuctionId(), ItemName));

            Assert.False(result.IsValid);
        }
    }
}