using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Events
{
    public class AuctionItemRemovedEventHandlerTests
    {
        private readonly InMemoryReadStore _readStore;
        private readonly AuctionItemRemovedEventHandler _handler;
        private readonly AuctionId _auctionId;

        public AuctionItemRemovedEventHandlerTests()
        {
            _readStore = new InMemoryReadStore();
            _auctionId = new AuctionId();
            _readStore.Add(new AuctionReadModel
            {
                Id = _auctionId,
                Name = "something",
                AuctionDate = DateTimeOffset.UtcNow
            });
            AddItemToAuction("idk");
            _readStore.SaveChanges();
            
            _handler = new AuctionItemRemovedEventHandler(_readStore);
        }

        [Fact]
        public async Task WhenAuctionItemRemovedThenItemIsDeletedFromAuction()
        {
            var @event = new AuctionItemRemovedEvent(_auctionId, "idk");
            
            await _handler.HandleAsync(@event);
            
            Assert.Empty(_readStore.GetAll<AuctionItemReadModel>());
        }

        [Fact]
        public async Task WhenAuctionItemRemovedThenOnlyTheCorrectAuctionItemIsRemoved()
        {
            AddItemToAuction("something else");

            var @event = new AuctionItemRemovedEvent(_auctionId, "something else");

            await _handler.HandleAsync(@event);

            Assert.Single(_readStore.GetAll<AuctionItemReadModel>());
        }

        private void AddItemToAuction(string somethingElse)
        {
            _readStore.Add(new AuctionItemReadModel
            {
                AuctionId = _auctionId,
                Name = somethingElse
            });
            _readStore.SaveChanges();
        }
    }
}