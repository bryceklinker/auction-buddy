using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.ViewModels;
using Auction.Buddy.Core.Common.Queries;
using Auction.Buddy.Core.Common.Storage;
using Auction.Buddy.Core.Common.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Core.Auctions.Queries
{
    public class GetAuctionItemsListQuery
    {
        public string AuctionId { get; }

        public GetAuctionItemsListQuery(string auctionId)
        {
            AuctionId = auctionId;
        }
    }
    public class GetAuctionItemsListQueryHandler : QueryHandler<GetAuctionItemsListQuery, ListViewModel<AuctionItemListItemViewModel>>
    {
        private readonly ReadStore _readStore;

        public GetAuctionItemsListQueryHandler(ReadStore readStore)
        {
            _readStore = readStore;
        }

        public async Task<ListViewModel<AuctionItemListItemViewModel>> HandleAsync(GetAuctionItemsListQuery query)
        {
            var items = await _readStore.GetAll<AuctionItemReadModel>()
                .Where(i => i.AuctionId == query.AuctionId)
                .Select(i => new AuctionItemListItemViewModel
                {
                    Description = i.Description,
                    Donor = i.Donor,
                    Name = i.Name,
                    Quantity = i.Quantity
                })
                .ToArrayAsync();

            return new ListViewModel<AuctionItemListItemViewModel>(items);
        }
    }
}