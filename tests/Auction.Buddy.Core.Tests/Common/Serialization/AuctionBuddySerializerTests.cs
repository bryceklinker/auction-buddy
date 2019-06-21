using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Harvest.Home.Core.Auctions.Create;
using Harvest.Home.Core.Common.Serialization;
using Xunit;

namespace Harvest.Home.Core.Tests.Common.Serialization
{
    public class AuctionBuddySerializerTests
    {
        private readonly AuctionBuddySerializer _serializer;

        public AuctionBuddySerializerTests()
        {
            _serializer = new AuctionBuddySerializer();
        }
        
        [Fact]
        public async Task GivenRequestWithJsonContentWhenDeserializedThenDataIsDeserializedCorrectly()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://something.com")
            {
                Content = new StringContent("{ \"auctionDate\": \"2019-11-02\" }", Encoding.UTF8, "applicatino/json")
            };

            var data = await _serializer.DeserializeAsync<CreateAuctionDto>(request);
            data.AuctionDate.Should().Be(new DateTimeOffset(2019, 11, 02, 0, 0, 0, TimeSpan.FromHours(-5)));
        }
    }
}