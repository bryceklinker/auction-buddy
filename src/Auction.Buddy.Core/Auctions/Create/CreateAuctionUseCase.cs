using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Harvest.Home.Core.Auctions.Entities;
using Harvest.Home.Core.Common.Storage;
using Harvest.Home.Core.Common.UseCases;

namespace Harvest.Home.Core.Auctions.Create
{
   
    public class CreateAuctionUseCase : IUseCase<CreateAuctionDto, ValidationResult>
    {
        private readonly IStorage _storage;
        private readonly IValidator<CreateAuctionDto> _validator;
        private readonly IMapper _mapper;

        public CreateAuctionUseCase(IStorage storage, IMapper mapper, IValidator<CreateAuctionDto> validator)
        {
            _storage = storage;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UseCaseResponse<CreateAuctionDto, ValidationResult>> ExecuteAsync(UseCaseRequest<CreateAuctionDto, ValidationResult> request)
        {
            var validationResult = await _validator.ValidateAsync(request.Input);
            if (!validationResult.IsValid)
                return request.CreateResponse(validationResult);

            var entity = _mapper.Map<AuctionEntity>(request.Input);
            _storage.AddEntity(entity);
            await _storage.SaveAsync();
            return request.CreateResponse(new ValidationResult());
        }
    }
}