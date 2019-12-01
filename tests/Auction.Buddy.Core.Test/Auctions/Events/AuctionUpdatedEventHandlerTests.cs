using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Events
{
    public class AuctionUpdatedEventHandlerTests
    {
        private readonly AuctionId _auctionId;
        private readonly InMemoryReadStore _readStore;
        private readonly AuctionUpdatedEventHandler _handler;
        private readonly DateTimeOffset _originalAuctionDate;

        public AuctionUpdatedEventHandlerTests()
        {
            _auctionId = new AuctionId();
            _readStore = new InMemoryReadStore();
            _originalAuctionDate = DateTimeOffset.UtcNow;
            _readStore.Add(new AuctionReadModel
            {
                Id = _auctionId,
                Name = "bob",
                AuctionDate = _originalAuctionDate
            });
            _readStore.SaveChanges();
            
            _handler = new AuctionUpdatedEventHandler(_readStore);
        }

        [Fact]
        public async Task WhenAuctionIsUpdatedThenAuctionReadModelIsUpdated()
        {
            var @event = new AuctionUpdatedEvent(_auctionId, "new-hotness", DateTimeOffset.UtcNow);

            await _handler.HandleAsync(@event);

            var auction = _readStore.GetAll<AuctionReadModel>().Single();
            Assert.Equal("new-hotness", auction.Name);
            Assert.Equal(@event.AuctionDate, auction.AuctionDate);
        }

        [Fact]
        public async Task WhenOnlyNameIsUpdatedThenAuctionDateIsNotUpdated()
        {
            var @event = new AuctionUpdatedEvent(_auctionId, "new-hotness", null);

            await _handler.HandleAsync(@event);

            var auction = _readStore.GetAll<AuctionReadModel>().Single();
            Assert.Equal("new-hotness", auction.Name);
            Assert.Equal(_originalAuctionDate, auction.AuctionDate);
        }

        [Fact]
        public async Task WhenOnlyAuctionDateIsUpdatedThenNameIsNotUpdated()
        {
            var @event = new AuctionUpdatedEvent(_auctionId, null, DateTimeOffset.UtcNow);

            await _handler.HandleAsync(@event);

            var auction = _readStore.GetAll<AuctionReadModel>().Single();
            Assert.Equal("bob", auction.Name);
            Assert.Equal(@event.AuctionDate, auction.AuctionDate);
        }
    }
}