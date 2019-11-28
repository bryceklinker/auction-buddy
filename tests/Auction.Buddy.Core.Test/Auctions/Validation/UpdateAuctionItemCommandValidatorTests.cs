using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Validation
{
    public class UpdateAuctionItemCommandValidatorTests
    {
        private const string ItemName = "item";
        private readonly AuctionId _existingAuctionId;
        private readonly UpdateAuctionItemCommandValidator _validator;

        public UpdateAuctionItemCommandValidatorTests()
        {
            var auction = new AuctionAggregateFactory().Create("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem(ItemName, "donor"));
            _existingAuctionId = auction.Id;

            var eventStore = new InMemoryEventStore();
            eventStore.Commit(auction.Id, auction.Changes);
            
            _validator = new UpdateAuctionItemCommandValidator(eventStore);
        }

        [Fact]
        public async Task WhenOldItemNameIsNullThenReturnsInvalid()
        {
            var command = new UpdateAuctionItemCommand(_existingAuctionId, null);
            var result = await _validator.ValidateAsync(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenAuctionIdDoesNotExistThenReturnsInvalid()
        {
            var command = new UpdateAuctionItemCommand(new AuctionId(), ItemName);
            var result = await _validator.ValidateAsync(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenQuantityIsZeroThenReturnsInvalid()
        {
            var command = new UpdateAuctionItemCommand(_existingAuctionId, ItemName, quantity: 0);
            var result = await _validator.ValidateAsync(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenQuantityIsNegativeThenReturnsInvalid()
        {
            var command = new UpdateAuctionItemCommand(_existingAuctionId, ItemName, quantity: -1);
            var result = await _validator.ValidateAsync(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenQuantityIsNullThenReturnsValid()
        {
            var command = new UpdateAuctionItemCommand(_existingAuctionId, ItemName);
            var result = await _validator.ValidateAsync(command);

            Assert.True(result.IsValid);
        }
    }
}