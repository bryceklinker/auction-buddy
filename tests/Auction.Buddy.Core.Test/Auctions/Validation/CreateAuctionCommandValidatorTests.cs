using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Auctions.Validation;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Validation
{
    public class CreateAuctionCommandValidatorTests
    {
        private readonly CreateAuctionCommandValidator _validator;

        public CreateAuctionCommandValidatorTests()
        {
            _validator = new CreateAuctionCommandValidator();
        }

        [Fact]
        public async Task WhenNameIsNullThenInvalid()
        {
            var result = await _validator.ValidateAsync(new CreateAuctionCommand(null, DateTimeOffset.UtcNow));

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenNameIsEmptyThenInvalid()
        {
            var result = await _validator.ValidateAsync(new CreateAuctionCommand(string.Empty, DateTimeOffset.UtcNow));

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenAuctionDateIsInThePastThenInvalid()
        {
            var command = new CreateAuctionCommand("one", DateTimeOffset.UtcNow.AddDays(-1));
            
            var result = await _validator.ValidateAsync(command);

            Assert.False(result.IsValid);
        }
    }
}