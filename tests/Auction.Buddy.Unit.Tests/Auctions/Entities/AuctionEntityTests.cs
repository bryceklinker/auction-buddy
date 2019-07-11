using System;
using Auction.Buddy.Core.Auctions.Entities;
using FluentAssertions;
using Xunit;

namespace Auction.Buddy.Unit.Tests.Auctions.Entities
{
    public class AuctionEntityTests
    {
        [Fact]
        public void GivenAuctionEntityWhenConvertedToDtoThenAuctionDtoIsPopulatedFromAuctionEntity()
        {
            var entity = new AuctionEntity(5, "something", new DateTime(2019, 6, 1));
            
            var dto = entity.ToDto();
            dto.Id.Should().Be(5);
            dto.Name.Should().Be("something");
            dto.AuctionDate.Should().Be("2019-06-01");
        }

        [Fact]
        public void GivenAuctionEntityWhenConvertedToDtoUsingExpressionThenDtoIsPopulatedFromAuctionEntity()
        {
            var entity = new AuctionEntity(43, "some", new DateTime(2016, 6, 8));

            var dto = AuctionEntity.DtoExpression.Compile().Invoke(entity);
            dto.Id.Should().Be(43);
            dto.Name.Should().Be("some");
            dto.AuctionDate.Should().Be("2016-06-08");
        }
    }
}