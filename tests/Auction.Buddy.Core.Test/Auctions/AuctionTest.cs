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
        private readonly InMemoryEventStore _eventStore;
        private readonly Core.Auctions.Auction _auction;

        public AuctionTest()
        {
            _auction = new Core.Auctions.Auction("one", DateTimeOffset.UtcNow);
            _eventStore = new InMemoryEventStore();
        }
        
        [Fact]
        public void WhenCreatedThenCreatedEventIsStored()
        {
            _auction.CommitAsync(_eventStore);

            var domainEvent = (AuctionCreatedEvent) _eventStore.GetLastEvent(_auction.Id);
            Assert.Equal("one", domainEvent.Name);
            Assert.Equal(_auction.AuctionDate, domainEvent.AuctionDate);
        }

        [Fact]
        public void WhenCreatedThenAuctionIsPopulated()
        {
            var auctionDate = DateTimeOffset.UtcNow;
            var auction = new Core.Auctions.Auction("one", auctionDate);

            Assert.Equal("one", auction.Name);
            Assert.Equal(auctionDate, auction.AuctionDate);
        }

        [Fact]
        public async Task WhenItemAddedThenAuctionItemAddedEventIsStored()
        {
            await _auction.CommitAsync(_eventStore);

            var auctionItem = new AuctionItem("Name", "donor","Description", 3);
            _auction.AddAuctionItem(auctionItem);
            await _auction.CommitAsync(_eventStore);

            var addedEvent = (AuctionItemAddedEvent) _eventStore.GetLastEvent(_auction.Id);
            Assert.Equal(_auction.Id, addedEvent.AggregateId);
            Assert.Equal(auctionItem, addedEvent.Item);
        }

        [Fact]
        public void WhenItemAddedThenItemIsInTheAuctionsItems()
        {
            var auctionItem = new AuctionItem("Name", "donor", "Description", 3);
            _auction.AddAuctionItem(auctionItem);

            Assert.Contains(auctionItem, _auction.Items);
        }

        [Fact]
        public async Task WhenItemWithTheSameNameIsAddedThenThrowsDuplicateItemException()
        {
            _auction.AddAuctionItem(new AuctionItem("one", "Bill", quantity: 4));
            await _auction.CommitAsync(_eventStore);
            
            var auctionItem = new AuctionItem("one", "bob");
            Assert.Throws<DuplicateAuctionItemException>(() => _auction.AddAuctionItem(auctionItem));
        }

        [Fact]
        public async Task WhenItemIsUpdatedThenAuctionItemUpdatedEventIsStored()
        {
            _auction.AddAuctionItem(new AuctionItem("one", "Bill", quantity: 4));
            
            _auction.UpdateAuctionItem("one", "three", "jack", "desc", 5);
            await _auction.CommitAsync(_eventStore);

            var addedEvent = (AuctionItemUpdatedEvent) _eventStore.GetLastEvent(_auction.Id);
            Assert.Equal(_auction.Id, addedEvent.AggregateId);
            Assert.Equal("one", addedEvent.OldName);
            Assert.Equal("three", addedEvent.NewName);
            Assert.Equal("jack", addedEvent.NewDonor);
            Assert.Equal("desc", addedEvent.NewDescription);
            Assert.Equal(5, addedEvent.NewQuantity);
        }
        
        [Fact]
        public void WhenItemQuantityIsUpdatedThenItemHasQuantityAdjusted()
        {
            _auction.AddAuctionItem(new AuctionItem("one", "Bill", quantity: 4));
            _auction.UpdateAuctionItem("one", "other", "Sue", "simple", 50);

            var updatedItem = _auction.Items.First();
            Assert.Equal("other", updatedItem.Name);
            Assert.Equal("Sue", updatedItem.Donor);
            Assert.Equal("simple", updatedItem.Description);
            Assert.Equal(50, updatedItem.Quantity);
        }

        [Fact]
        public void WhenMissingItemQuantityIsUpdatedThenMissingItemExceptionIsThrown()
        {
            _auction.AddAuctionItem(new AuctionItem("one", "Bill", quantity: 4));

            Assert.Throws<MissingAuctionItemException>(() => _auction.UpdateAuctionItem("three", "idk"));
        }

        [Fact]
        public async Task WhenUpdatingAuctionThenAuctionUpdatedEventIsStored()
        {
            var auctionDate = new DateTimeOffset(2019, 3, 26, 8, 2, 12, 0, TimeSpan.Zero);
            _auction.UpdateAuction("three", auctionDate);
            await _auction.CommitAsync(_eventStore);

            var @event = (AuctionUpdatedEvent) _eventStore.GetLastEvent(_auction.Id);
            Assert.Equal(_auction.Id, @event.AggregateId);
            Assert.Equal("three", @event.Name);
            Assert.Equal(auctionDate, @event.AuctionDate);
        }

        [Fact]
        public void WhenAuctionUpdatedThenAuctionInformationReflectsChange()
        {
            var auctionDate = new DateTimeOffset(2019, 3, 26, 8, 2, 12, 0, TimeSpan.Zero);
            _auction.UpdateAuction("three", auctionDate);

            Assert.Equal("three", _auction.Name);
            Assert.Equal(auctionDate, _auction.AuctionDate);
        }

        [Fact]
        public async Task WhenAuctionItemRemovedThenAuctionItemRemovedEventIsStored()
        {
            _auction.AddAuctionItem(new AuctionItem("some", "Bill", quantity: 4));

            _auction.RemoveAuctionItem("some");
            await _auction.CommitAsync(_eventStore);

            var @event = (AuctionItemRemovedEvent) _eventStore.GetLastEvent(_auction.Id);
            Assert.Equal(_auction.Id, @event.AggregateId);
            Assert.Equal("some", @event.Name);
        }

        [Fact]
        public void WhenAuctionItemRemovedThenAuctionItemIsNoLongerInAuctionItems()
        {
            _auction.AddAuctionItem(new AuctionItem("some", "Bill", quantity: 4));

            _auction.RemoveAuctionItem("some");

            Assert.Empty(_auction.Items);
        }

        [Fact]
        public void WhenMissingItemIsRemovedThenMissingAuctionItemIsThrown()
        {
            Assert.Throws<MissingAuctionItemException>(() => _auction.RemoveAuctionItem("blah"));
        }

        [Fact]
        public async Task WhenCommittedThenChangesAreAddedToEventStore()
        {
            _auction.AddAuctionItem(new AuctionItem("some", "bill"));
            _auction.AddAuctionItem(new AuctionItem("jack", "bill"));

            await _auction.CommitAsync(_eventStore);

            var events = _eventStore.GetEventsById(_auction.Id);
            Assert.Equal(3, events.Length);
        }
    }
}