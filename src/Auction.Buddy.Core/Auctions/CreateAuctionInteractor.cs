using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Dtos;

namespace Auction.Buddy.Core.Auctions
{
    public interface ICreateAuctionInteractor
    {
        Task<AuctionDto> ExecuteAsync(CreateAuctionDto dto);
    }

    public class CreateAuctionInteractor : ICreateAuctionInteractor
    {
        private readonly IAuctionEntityFactory _factory;
        private readonly IAuctionEntityRepository _repository;

        public CreateAuctionInteractor(IAuctionEntityFactory factory, IAuctionEntityRepository repository)
        {
            _factory = factory;
            _repository = repository;
        }

        public async Task<AuctionDto> ExecuteAsync(CreateAuctionDto dto)
        {
            var entity = await _factory.Create(dto);
            var savedEntity = await _repository.AddAsync(entity);
            return savedEntity.ToDto();
        } 
    }
}