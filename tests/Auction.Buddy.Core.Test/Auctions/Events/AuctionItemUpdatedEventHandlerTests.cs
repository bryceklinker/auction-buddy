using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Events
{
    public class AuctionItemUpdatedEventHandlerTests
    {
        private readonly AuctionId _auctionId;
        private readonly InMemoryReadStore _readStore;
        private readonly AuctionItemUpdatedEventHandler _handler;

        public AuctionItemUpdatedEventHandlerTests()
        {
            _auctionId = new AuctionId();
            
            _readStore = new InMemoryReadStore();
            _readStore.AddAuction(_auctionId, "idk");
            _readStore.AddAuctionItem(_auctionId, "existing", "don", "what");
            
            _handler = new AuctionItemUpdatedEventHandler(_readStore);
        }

        [Fact]
        public async Task WhenAuctionItemIsUpdatedThenReadAuctionItemIsUpdatedFromEvent()
        {
            var @event = new AuctionItemUpdatedEvent(
                _auctionId,
                "existing",
                "newhotness",
                "new donor",
                "descripty",
                12);

            await _handler.HandleAsync(@event);

            var item = _readStore.GetAll<AuctionItemReadModel>().Single();
            Assert.Equal("newhotness", item.Name);
            Assert.Equal("new donor", item.Donor);
            Assert.Equal("descripty", item.Description);
            Assert.Equal(12, item.Quantity);
        }

        [Fact]
        public async Task WhenOnlyQuantityIsUpdatedThenOnlyQuantityIsUpdatedInReadModel()
        {
            var @event = new AuctionItemUpdatedEvent(_auctionId, "existing", newQuantity: 43);

            await _handler.HandleAsync(@event);

            var item = _readStore.GetAll<AuctionItemReadModel>().Single();
            Assert.Equal(43, item.Quantity);
            Assert.Equal("existing", item.Name);
            Assert.Equal("what", item.Description);
            Assert.Equal("don", item.Donor);
        }

        [Fact]
        public async Task WhenOnlyDescriptionIsUpdatedThenOnlyDescriptionIsUpdatedInReadModel()
        {
            var @event = new AuctionItemUpdatedEvent(_auctionId, "existing", newDescription: "star wars");

            await _handler.HandleAsync(@event);

            var item = _readStore.GetAll<AuctionItemReadModel>().Single();
            Assert.Equal(1, item.Quantity);
            Assert.Equal("existing", item.Name);
            Assert.Equal("star wars", item.Description);
            Assert.Equal("don", item.Donor);
        }
    }
}