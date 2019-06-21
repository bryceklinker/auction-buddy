using System;
using System.Threading.Tasks;
using FluentAssertions;
using Harvest.Home.Core.Auctions.Create;
using Xunit;

namespace Harvest.Home.Core.Tests.Auctions.Create
{
    public class CreateAuctionValidatorTests
    {
        private readonly CreateAuctionDto _dto;
        private readonly CreateAuctionValidator _validator;

        public CreateAuctionValidatorTests()
        {
            _dto = new CreateAuctionDto
            {
                Name = "Something",
                AuctionDate = DateTimeOffset.UtcNow
            };
            
            _validator = new CreateAuctionValidator();
        }
        
        [Fact]
        public async Task GivenAuctionWithEmptyNameThenAuctionIsNotValid()
        {
            _dto.Name = "";

            await AssertIsNotValid();
        }

        [Fact]
        public async Task GivenAuctionWithoutNameThenAuctionIsNotValid()
        {
            _dto.Name = null;

            await AssertIsNotValid();
        }

        [Fact]
        public async Task GivenAuctionWithPastDateThenAuctionIsNotValid()
        {
            _dto.AuctionDate = DateTimeOffset.UtcNow.AddMonths(-1);

            await AssertIsNotValid();
        }

        [Fact]
        public async Task GivenAuctionWithNameAndDateThenAuctionIsValid()
        {
            _dto.Name = "Harvest Home";
            _dto.AuctionDate = DateTimeOffset.UtcNow.AddMonths(1);

            await AssertIsValid();
        }

        private async Task AssertIsValid()
        {
            var result = await _validator.ValidateAsync(_dto);
            result.IsValid.Should().BeTrue();
        }

        private async Task AssertIsNotValid()
        {
            var result = await _validator.ValidateAsync(_dto);
            result.IsValid.Should().BeFalse();
        }
    }
}