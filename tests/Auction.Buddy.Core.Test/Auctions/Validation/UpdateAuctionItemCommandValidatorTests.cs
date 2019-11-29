using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Validation
{
    public class UpdateAuctionItemCommandValidatorTests : IAsyncLifetime
    {
        private const string ItemName = "item";
        private AuctionId _existingAuctionId;
        private UpdateAuctionItemCommandValidator _validator;

        public async Task InitializeAsync()
        {
            var auction = new Core.Auctions.Auction("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem(ItemName, "donor"));
            _existingAuctionId = auction.Id;

            var eventStore = new InMemoryEventStore();
            await auction.CommitAsync(eventStore);
            
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

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}