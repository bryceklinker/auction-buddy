using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions;
using Auction.Buddy.Core.Auctions.Queries;
using Auction.Buddy.Core.Test.Support.Storage;
using Xunit;

namespace Auction.Buddy.Core.Test.Auctions.Queries
{
    public class GetAuctionItemsListQueryHandlerTests
    {
        private readonly InMemoryReadStore _readStore;
        private readonly GetAuctionItemsListQueryHandler _handler;

        public GetAuctionItemsListQueryHandlerTests()
        {
            _readStore = new InMemoryReadStore();
            _handler = new GetAuctionItemsListQueryHandler(_readStore);
        }

        [Fact]
        public async Task WhenGettingAuctionItemsThenReturnsItemsMatchingAuctionId()
        {
            var auctionId = new AuctionId();
            _readStore.AddAuction(auctionId);
            _readStore.AddAuctionItem(auctionId, "shirts", "donor");
            _readStore.AddAuctionItem(auctionId, "shoes", "donor");
            _readStore.AddAuctionItem(auctionId, "shorts", "donor");

            var otherAuctionId = new AuctionId();
            _readStore.AddAuction(otherAuctionId);
            _readStore.AddAuctionItem(otherAuctionId, "nope", "bob");

            var viewModel = await _handler.HandleAsync(new GetAuctionItemsListQuery(auctionId));

            Assert.Equal(3, viewModel.Items.Length);
        }

        [Fact]
        public async Task WhenGettingAuctionItemsThenReturnsPopulatedAuctionItems()
        {
            var auctionId = new AuctionId();
            _readStore.AddAuction(auctionId);
            _readStore.AddAuctionItem(auctionId, "shirts", "donor", "desc", 54);

            var viewModel = await _handler.HandleAsync(new GetAuctionItemsListQuery(auctionId));

            var itemViewModel = viewModel.Items[0];
            Assert.Equal("shirts", itemViewModel.Name);
            Assert.Equal("donor", itemViewModel.Donor);
            Assert.Equal("desc", itemViewModel.Description);
            Assert.Equal(54, itemViewModel.Quantity);
        }
    }
}