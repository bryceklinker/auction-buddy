using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Queries;
using Auction.Buddy.Core.Test.Support.Queries;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Auction.Buddy.Core.Test.Common.Queries
{
    public class ServiceProviderQueryBusTests
    {
        private readonly ServiceProviderQueryBus _bus;

        public ServiceProviderQueryBusTests()
        {
            var provider = new ServiceCollection()
                .AddAuctionBuddy()
                .BuildServiceProvider();
            _bus = new ServiceProviderQueryBus(provider);
        }

        [Fact]
        public async Task WhenQueryIsExecutedThenHandlerIsCalled()
        {
            var query = new TestingQuery(Guid.NewGuid());

            var result = await _bus.ExecuteAsync<TestingQuery, object>(query);

            Assert.Equal(query.Result, result);
            Assert.True(query.WasHandled);
        }
    }
}