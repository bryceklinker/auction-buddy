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
    }
}