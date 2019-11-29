using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Validation
{
    public class RemoveAuctionItemCommandValidatorTests : IAsyncLifetime
    {
        private const string ItemName = "item";
        private AuctionId _existingAuctionId;
        private RemoveAuctionItemCommandValidator _validator;

        public async Task InitializeAsync()
        {
            var auction = new Core.Auctions.Auction("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem(ItemName, "donor"));
            _existingAuctionId = auction.Id;

            var eventStore = new InMemoryEventStore();
            await auction.CommitAsync(eventStore);
            
            _validator = new RemoveAuctionItemCommandValidator(eventStore);
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

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}