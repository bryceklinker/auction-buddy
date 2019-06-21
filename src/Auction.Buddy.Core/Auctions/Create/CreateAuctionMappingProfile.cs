using AutoMapper;
using Harvest.Home.Core.Auctions.Entities;

namespace Harvest.Home.Core.Auctions.Create
{
    public class CreateAuctionMappingProfile : Profile
    {
        public CreateAuctionMappingProfile()
        {
            CreateMap<CreateAuctionDto, AuctionEntity>();
        }
    }
}