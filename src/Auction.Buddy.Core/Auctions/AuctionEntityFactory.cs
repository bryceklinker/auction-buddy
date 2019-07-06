using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Dtos;
using Auction.Buddy.Core.Auctions.Entities;
using Auction.Buddy.Core.Auctions.Validators;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions
{
    public interface IAuctionEntityFactory
    {
        Task<AuctionEntity> Create(CreateAuctionDto dto);
    }

    public class AuctionEntityFactory : IAuctionEntityFactory
    {
        private readonly IValidator<CreateAuctionDto> _validator;

        public AuctionEntityFactory()
        {
            _validator = new CreateAuctionDtoValidator();
        }
        public async Task<AuctionEntity> Create(CreateAuctionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            
            await _validator.ValidateAndThrowAsync(dto);
            return new AuctionEntity(dto.Name, DateTime.Parse(dto.AuctionDate));
        }
    }
}