using System;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Auctions.Exceptions;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions
{
    public class AuctionTest
    {
        private readonly AuctionFactory _auctionFactory = new AuctionAggregateFactory();
        private readonly InMemoryEventStore _eventStore;

        public AuctionTest()
        {
            _eventStore = new InMemoryEventStore();
        }
        
        [Fact]
        public void WhenCreatedThenCreatedEventIsInChanges()
        {
            var auctionDate = DateTimeOffset.UtcNow;
            var auction = _auctionFactory.Create("one", auctionDate);

            var changes = auction.Changes.ToArray();
            Assert.Single(changes);

            var domainEvent = (AuctionCreatedEvent)changes[0];
            Assert.Equal("one", domainEvent.Name);
            Assert.Equal(auctionDate, domainEvent.AuctionDate);
        }

        [Fact]
        public void WhenCreatedThenAuctionIsPopulated()
        {
            var auctionDate = DateTimeOffset.UtcNow;
            var auction = _auctionFactory.Create("one", auctionDate);

            Assert.Equal("one", auction.Name);
            Assert.Equal(auctionDate, auction.AuctionDate);
        }

        [Fact]
        public async Task WhenItemAddedThenAuctionItemAddedIsInChanges()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow));
            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);

            var auctionItem = new AuctionItem("Name", "donor","Description", 3);
            auction.AddAuctionItem(auctionItem);

            Assert.Single(auction.Changes);
            
            var addedEvent = (AuctionItemAddedEvent)auction.Changes.Single();
            Assert.Equal(id, addedEvent.AggregateId);
            Assert.Equal(auctionItem, addedEvent.Item);
        }

        [Fact]
        public async Task WhenItemAddedThenItemIsInTheAuctionsItems()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow));
            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);

            var auctionItem = new AuctionItem("Name", "donor", "Description", 3);
            auction.AddAuctionItem(auctionItem);

            Assert.Contains(auctionItem, auction.Items);
        }

        [Fact]
        public async Task WhenItemWithTheSameNameIsAddedThenThrowsDuplicateItemException()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, 
                new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow),
                new AuctionItemAddedEvent(id, new AuctionItem("one", "Bill", quantity: 4))
            );
            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);
            
            var auctionItem = new AuctionItem("one", "bob");
            Assert.Throws<DuplicateAuctionItemException>(() => auction.AddAuctionItem(auctionItem));
        }

        [Fact]
        public async Task WhenItemIsUpdatedThenAuctionHasAuctionItemUpdatedEvent()
        {
            var id = new AuctionId(); 
            _eventStore.Commit(id, 
                new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow),
                new AuctionItemAddedEvent(id, new AuctionItem("one", "Bill", quantity: 4)));
            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);
            
            auction.UpdateAuctionItem("one", "three", "jack", "desc", 5);

            Assert.Single(auction.Changes);
            
            var addedEvent = (AuctionItemUpdatedEvent)auction.Changes.Single();
            Assert.Equal(id, addedEvent.AggregateId);
            Assert.Equal("one", addedEvent.OldName);
            Assert.Equal("three", addedEvent.NewName);
            Assert.Equal("jack", addedEvent.NewDonor);
            Assert.Equal("desc", addedEvent.NewDescription);
            Assert.Equal(5, addedEvent.NewQuantity);
        }
        
        [Fact]
        public async Task WhenItemQuantityIsUpdatedThenItemHasQuantityAdjusted()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, 
                new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow),
                new AuctionItemAddedEvent(id, new AuctionItem("one", "Bill", quantity: 4)));

            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);

            auction.UpdateAuctionItem("one", "other", "Sue", "simple", 50);

            var updatedItem = auction.Items.First();
            Assert.Equal("other", updatedItem.Name);
            Assert.Equal("Sue", updatedItem.Donor);
            Assert.Equal("simple", updatedItem.Description);
            Assert.Equal(50, updatedItem.Quantity);
        }

        [Fact]
        public async Task WhenMissingItemQuantityIsUpdatedThenMissingItemExceptionIsThrown()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, 
                new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow),
                new AuctionItemAddedEvent(id, new AuctionItem("one", "Bill", quantity: 4)));

            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);

            Assert.Throws<MissingAuctionItemException>(() => auction.UpdateAuctionItem("three", "idk"));
        }

        [Fact]
        public async Task WhenUpdatingAuctionThenAuctionUpdatedEventIsInChanges()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow));
            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);

            var auctionDate = new DateTimeOffset(2019, 3, 26, 8, 2, 12, 0, TimeSpan.Zero);
            auction.UpdateAuction("three", auctionDate);

            Assert.Single(auction.Changes);

            var @event = (AuctionUpdatedEvent) auction.Changes.Single();
            Assert.Equal(id, @event.AggregateId);
            Assert.Equal("three", @event.Name);
            Assert.Equal(auctionDate, @event.AuctionDate);
        }

        [Fact]
        public async Task WhenAuctionUpdatedThenAuctionInformationReflectsChange()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow));
            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);

            var auctionDate = new DateTimeOffset(2019, 3, 26, 8, 2, 12, 0, TimeSpan.Zero);
            auction.UpdateAuction("three", auctionDate);

            Assert.Equal("three", auction.Name);
            Assert.Equal(auctionDate, auction.AuctionDate);
        }

        [Fact]
        public async Task WhenAuctionItemRemovedThenAuctionItemRemovedIsInChanges()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, 
                new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow),
                new AuctionItemAddedEvent(id, new AuctionItem("some", "bill"))
            );

            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);

            auction.RemoveAuctionItem("some");

            Assert.Single(auction.Changes);

            var @event = (AuctionItemRemovedEvent) auction.Changes.Single();
            Assert.Equal(id, @event.AggregateId);
            Assert.Equal("some", @event.Name);
        }

        [Fact]
        public async Task WhenAuctionItemRemovedThenAuctionItemIsNoLongerInAuctionItems()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, 
                new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow),
                new AuctionItemAddedEvent(id, new AuctionItem("some", "bill"))
            );

            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);

            auction.RemoveAuctionItem("some");

            Assert.Empty(auction.Items);
        }

        [Fact]
        public async Task WhenMissingItemIsRemovedThenMissingAuctionItemIsThrown()
        {
            var id = new AuctionId();
            _eventStore.Commit(id, 
                new AuctionCreatedEvent(id, "one", DateTimeOffset.UtcNow)
            );

            var auction = await _eventStore.LoadAggregateAsync<Core.Auctions.Auction, AuctionId>(id);
            
            Assert.Throws<MissingAuctionItemException>(() => auction.RemoveAuctionItem("blah"));
        }

        [Fact]
        public async Task WhenCommittedThenChangesAreAddedToEventStore()
        {
            
            
            var auction = _auctionFactory.Create("one", DateTimeOffset.UtcNow);
            auction.AddAuctionItem(new AuctionItem("some", "bill"));
            auction.AddAuctionItem(new AuctionItem("jack", "bill"));

            await auction.CommitAsync(_eventStore);

            var events = _eventStore.GetEventsById(auction.Id);
            Assert.Equal(3, events.Length);
        }
    }
}