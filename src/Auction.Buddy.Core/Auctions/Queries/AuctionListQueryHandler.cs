using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.ViewModels;
using Auction.Buddy.Core.Common.Queries;
using Auction.Buddy.Core.Common.Storage;
using Auction.Buddy.Core.Common.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Auction.Buddy.Core.Auctions.Queries
{
    public class AuctionListQuery
    {
    }

    public class AuctionListQueryHandler : QueryHandler<AuctionListQuery, ListViewModel<AuctionListItemViewModel>>
    {
        private readonly ReadStore _readStore;

        public AuctionListQueryHandler(ReadStore readStore)
        {
            _readStore = readStore;
        }

        public async Task<ListViewModel<AuctionListItemViewModel>> HandleAsync(AuctionListQuery query)
        {
            var auctions = await _readStore.GetAll<AuctionReadModel>()
                .Include(a => a.Items)
                .Select(a => new AuctionListItemViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    AuctionDate = a.AuctionDate,
                    ItemCount = a.Items.Count
                })
                .ToArrayAsync();
            
            return new ListViewModel<AuctionListItemViewModel>
            {
                Items = auctions
            };
        }
    }
}