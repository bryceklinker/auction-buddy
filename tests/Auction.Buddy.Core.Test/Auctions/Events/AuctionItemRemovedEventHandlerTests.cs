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
            _auctionId = new AuctionId();
            
            _readStore = new InMemoryReadStore();
            _readStore.AddAuction(_auctionId, "something");
            _readStore.AddAuctionItem(_auctionId, "idk");
            
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
            _readStore.AddAuctionItem(_auctionId, "something else");

            var @event = new AuctionItemRemovedEvent(_auctionId, "something else");

            await _handler.HandleAsync(@event);

            Assert.Single(_readStore.GetAll<AuctionItemReadModel>());
        }
    }
}