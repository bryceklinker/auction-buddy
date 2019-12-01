using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Events;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Events
{
    public class AuctionItemAddedEventHandlerTests
    {
        private readonly AuctionId _auctionId;
        private readonly InMemoryReadStore _readStore;
        private readonly AuctionItemAddedEventHandler _handler;

        public AuctionItemAddedEventHandlerTests()
        {
            _readStore = new InMemoryReadStore();
            _auctionId = new AuctionId();
            _readStore.AddAuction(_auctionId, "New");
            
            _handler = new AuctionItemAddedEventHandler(_readStore);
        }

        [Fact]
        public async Task WhenAuctionItemAddedThenItemIsAddedToAuctionReadModel()
        {
            var @event = new AuctionItemAddedEvent(_auctionId, new AuctionItem("something", "boy"));

            await _handler.HandleAsync(@event);

            Assert.Single(_readStore.GetAll<AuctionItemReadModel>());
        }

        [Fact]
        public async Task WhenAuctionItemAddedThenItemIsPopulatedFromEvent()
        {
            var @event = new AuctionItemAddedEvent(_auctionId, new AuctionItem("something", "boy", "description", 65));

            await _handler.HandleAsync(@event);

            var item = _readStore.GetAll<AuctionItemReadModel>().Single();
            Assert.Equal(_auctionId.ToString(), item.AuctionId);
            Assert.Equal("something", item.Name);
            Assert.Equal("boy", item.Donor);
            Assert.Equal("description", item.Description);
            Assert.Equal(65, item.Quantity);
        }
    }
}