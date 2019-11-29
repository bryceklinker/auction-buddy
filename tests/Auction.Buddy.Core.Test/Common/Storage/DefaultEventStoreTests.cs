using System;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Common.Storage;
using Auction.Buddy.Core.Test.Support.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Common.Storage
{
    public class DefaultEventStoreTests
    {
        private readonly InMemoryDomainEventBus _eventBus;
        private readonly DefaultEventStore _eventStore;
        private readonly InMemoryEventPersistence _eventPersistence;

        public DefaultEventStoreTests()
        {
            _eventBus = new InMemoryDomainEventBus();
            _eventPersistence = new InMemoryEventPersistence();
            _eventStore = new DefaultEventStore(_eventPersistence, _eventBus);
        }

        [Fact]
        public async Task WhenCommittingEventsThenPersistenceEventsArePersisted()
        {
            var auction = new Core.Auctions.Auction("three", DateTimeOffset.UtcNow);
            await auction.CommitAsync(_eventStore);

            var persistedEvents = await _eventPersistence.LoadEventsAsync(auction.Id);
            Assert.Single(persistedEvents);
        }

        [Fact]
        public async Task WhenCommittingEventsThenEachEventIsPublished()
        {
            var auction = new Core.Auctions.Auction("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem("computer", "jack"));
            auction.AddAuctionItem(new AuctionItem("bob", "jack"));
            await auction.CommitAsync(_eventStore);

            Assert.Single(_eventBus.GetPublishedEvent<AuctionCreatedEvent, AuctionId>());
            Assert.Equal(2, _eventBus.GetPublishedEvent<AuctionItemAddedEvent, AuctionId>().Length);
        }

        [Fact]
        public async Task WhenGettingAllEventsForIdThenReturnsAllDomainEventsWithId()
        {
            var auction = new Core.Auctions.Auction("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem("shoes", "joel"));
            await auction.CommitAsync(_eventStore);

            var events = await _eventStore.GetEventsByIdAsync(auction.Id);
            Assert.Equal(2, events.Length);
        }

        [Fact]
        public async Task WhenGettingAllEventsForIdThenPopulatesDomainEventFromPersistedEvent()
        {
            var auction = new Core.Auctions.Auction("one", DateTimeOffset.UtcNow);
            await auction.CommitAsync(_eventStore);

            var events = await _eventStore.GetEventsByIdAsync(auction.Id);
            Assert.Single(events.OfType<AuctionCreatedEvent>());
        }

        [Fact]
        public async Task WhenLoadingAggregateThenAggregateIsPopulatedFromEvents()
        {
            var auction = new Core.Auctions.Auction("three", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem("hat", "joel"));
            await auction.CommitAsync(_eventStore);

            var aggregate = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(auction.Id);
            Assert.Equal(auction.Id, aggregate.Id);
            Assert.Equal("three", aggregate.Name);
            Assert.Equal(auction.AuctionDate, aggregate.AuctionDate);
            Assert.Equal(auction.Items.First(), aggregate.Items.First());
        }

        [Fact]
        public async Task WhenLoadingAggregateThatDoesNotExistThenReturnsNull()
        {
            var aggregate = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(new AuctionId());

            Assert.Null(aggregate);
        }
    }
}