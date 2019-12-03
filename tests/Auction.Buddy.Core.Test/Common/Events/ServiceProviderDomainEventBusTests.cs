using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Events;
using Auction.Buddy.Core.Test.Support.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Auction.Buddy.Core.Test.Common.Events
{
    public class ServiceProviderDomainEventBusTests
    {
        private readonly ServiceProviderDomainEventBus _bus;

        public ServiceProviderDomainEventBusTests()
        {
            var provider = new ServiceCollection()
                .AddAuctionBuddy()
                .BuildServiceProvider();
            
            _bus = new ServiceProviderDomainEventBus(provider, new NullLoggerFactory());
        }

        [Fact]
        public async Task WhenEventIsPublishedThenHandlerIsPassedTheEvent()
        {
            var @event = new TestingDomainEvent(new TestingId());

            await _bus.PublishAsync<TestingDomainEvent, TestingId>(@event);

            Assert.Equal(1, @event.TimesHandled);
        }

        [Fact]
        public async Task WhenEventHasMultipleHandlersThenEachHandlerIsPassedTheEvent()
        {
            var @event = new TestingMultipleHandlersDomainEvent(new TestingId());

            await _bus.PublishAsync<TestingMultipleHandlersDomainEvent, TestingId>(@event);

            Assert.Equal(3, @event.TimesHandled);
        }
    }
}