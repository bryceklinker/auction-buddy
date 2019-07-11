using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Dtos;

namespace Auction.Buddy.Core.Auctions
{
    public interface IGetAllAuctionsInteractor
    {
        Task<AuctionDto[]> ExecuteAsync();
    }

    public class GetAllAuctionsInteractor : IGetAllAuctionsInteractor
    {
        private readonly IAuctionEntityRepository _repository;

        public GetAllAuctionsInteractor(IAuctionEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuctionDto[]> ExecuteAsync()
        {
            return await _repository.GetAllDtosAsync();
        }
    }
}