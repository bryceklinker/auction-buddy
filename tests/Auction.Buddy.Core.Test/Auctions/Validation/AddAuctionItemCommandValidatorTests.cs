using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Validation
{
    public class AddAuctionItemCommandValidatorTests : IAsyncLifetime
    {
        private AuctionId _existingAuctionId;
        private AddAuctionItemCommandValidator _validator;

        public async Task InitializeAsync()
        {
            var auction = new Core.Auctions.Auction("some", DateTimeOffset.UtcNow);
            
            var eventStore = new InMemoryEventStore();
            await auction.CommitAsync(eventStore);
            
            _existingAuctionId = auction.Id;
            
            _validator = new AddAuctionItemCommandValidator(eventStore);
        }

        [Fact]
        public async Task WhenAuctionItemIsNullThenReturnsInvalid()
        {
            var command = new AddAuctionItemCommand(_existingAuctionId, null);
            var result = await _validator.ValidateAsync(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenAuctionDoesNotExistThenReturnsInvalid()
        {
            var command = new AddAuctionItemCommand(new AuctionId(), new AuctionItem("one", "four"));
            var result = await _validator.ValidateAsync(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenAuctionIdIsNullThenReturnsInvalid()
        {
            var command = new AddAuctionItemCommand(null, new AuctionItem("one", "three"));
            var result = await _validator.ValidateAsync(command);

            Assert.False(result.IsValid);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}