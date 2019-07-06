using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Dtos;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Xunit;

namespace Auction.Buddy.Unit.Tests.Auctions
{
    public class AuctionEntityFactoryTests
    {
        private readonly AuctionEntityFactory _auctionEntityFactory;

        public AuctionEntityFactoryTests()
        {
            _auctionEntityFactory = new AuctionEntityFactory();
        }

        [Fact]
        public async Task GivenNullWhenCreateThenThrowsArgumentNullException()
        {
            await _auctionEntityFactory.Awaiting(f => f.Create(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GivenDtoWithInvalidDateWhenCreateThenThrowsValidationException()
        {
            var dto = new CreateAuctionDto
            {
                Name = "something",
                AuctionDate = "not-a-date"
            };

            await _auctionEntityFactory.Awaiting(f => f.Create(dto)).Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task GivenDtoWithNullNameWhenCreateThenThrowsValidationException()
        {
            var dto = new CreateAuctionDto {Name = null, AuctionDate = "2019-01-01"};
            await _auctionEntityFactory.Awaiting(f => f.Create(dto)).Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task GivenDtoWithEmptyNameWhenCreateThenThrowsValidationException()
        {
            var dto = new CreateAuctionDto { Name = string.Empty, AuctionDate = "2019-01-01"};
            await _auctionEntityFactory.Awaiting(f => f.Create(dto)).Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task GivenValidDtoWhenCreateThenReturnsPopulatedEntity()
        {
            var dto = new CreateAuctionDto{ Name = "Harvest Home", AuctionDate = "2019-11-11"};

            var entity = await _auctionEntityFactory.Create(dto);
            entity.Name.Should().Be("Harvest Home");
            entity.AuctionDate.Should().Be(new DateTime(2019, 11, 11));
        }
    }
}