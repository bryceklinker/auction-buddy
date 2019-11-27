using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Validation;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Validation
{
    public class AuctionItemValidatorTests
    {
        private readonly AuctionItemValidator _validator;

        public AuctionItemValidatorTests()
        {
            _validator = new AuctionItemValidator();
        }

        [Fact]
        public async Task WhenNameIsNullThenReturnsInvalid()
        {
            var result = await _validator.ValidateAsync(new AuctionItem(null, "something"));

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenAuctionItemDonorIsNullThenReturnsInvalid()
        {
            var result = await _validator.ValidateAsync(new AuctionItem("one", null));

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenQuantityIsZeroThenReturnsInvalid()
        {
            var result = await _validator.ValidateAsync(new AuctionItem("a", "b", quantity: 0));

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task WhenQuantityIsNegativeThenReturnsInvalid()
        {
            var result = await _validator.ValidateAsync(new AuctionItem("a", "b", quantity: -1));

            Assert.False(result.IsValid);
        }
    }
}