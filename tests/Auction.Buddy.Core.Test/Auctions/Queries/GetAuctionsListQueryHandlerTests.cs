using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Queries;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Queries
{
    public class GetAuctionsListQueryHandlerTests
    {
        private readonly InMemoryReadStore _readStore;
        private readonly GetAuctionsListQueryHandler _handler;

        public GetAuctionsListQueryHandlerTests()
        {
            _readStore = new InMemoryReadStore();
            _handler = new GetAuctionsListQueryHandler(_readStore);
        }

        [Fact]
        public async Task WhenListIsQueriedThenReturnsAllAuctions()
        {
            _readStore.AddAuction();
            _readStore.AddAuction();
            _readStore.AddAuction();

            var viewModel = await _handler.HandleAsync(new GetAuctionsListQuery());

            Assert.Equal(3, viewModel.Items.Length);
        }

        [Fact]
        public async Task WhenListIsQueriedThenPopulatesViewModelsFromReadModel()
        {
            var auctionId = new AuctionId();
            var auction = _readStore.AddAuction(auctionId);
            _readStore.AddAuctionItem(auctionId, "other");
            _readStore.AddAuctionItem(auctionId, "three");
            _readStore.AddAuctionItem(auctionId, "one");

            var viewModel = await _handler.HandleAsync(new GetAuctionsListQuery());

            var auctionListItem = viewModel.Items[0];
            Assert.Equal(auction.Id, auctionListItem.Id);
            Assert.Equal(auction.Name, auctionListItem.Name);
            Assert.Equal(auction.AuctionDate, auctionListItem.AuctionDate);
            Assert.Equal(3, auctionListItem.ItemCount);
        }
    }
}