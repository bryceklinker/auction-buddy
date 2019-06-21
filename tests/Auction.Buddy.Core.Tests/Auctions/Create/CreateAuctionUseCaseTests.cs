using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using Harvest.Home.Core.Auctions.Create;
using Harvest.Home.Core.Auctions.Entities;
using Harvest.Home.Core.Common.Storage;
using Harvest.Home.Core.Common.UseCases;
using Harvest.Home.Core.Tests.Support;
using Xunit;

namespace Harvest.Home.Core.Tests.Auctions.Create
{
    public class CreateAuctionUseCaseTests
    {
        private readonly IStorage _storage;
        private readonly CreateAuctionUseCase _useCase;
        private readonly UseCaseRequest<CreateAuctionDto, ValidationResult> _request;

        public CreateAuctionUseCaseTests()
        {
            _storage = InMemoryStorageFactory.Create();

            _request = new UseCaseRequest<CreateAuctionDto, ValidationResult>(new CreateAuctionDto
            {
                Name = "Harvest Home",
                AuctionDate = DateTimeOffset.UtcNow
            });
            _useCase = new CreateAuctionUseCase(_storage, MapperFactory.Create(), new CreateAuctionValidator());
        }

        [Fact]
        public async Task GivenAuctionToCreateWhenExecuteThenNewAuctionIsSaved()
        {
            await _useCase.ExecuteAsync(_request);
            _storage.GetAll<AuctionEntity>().Should().HaveCount(1);
        }

        [Fact]
        public async Task GivenAuctionToCreateWhenExecuteThenNewAuctionIsPopulatedFromDto()
        {
            await _useCase.ExecuteAsync(_request);

            var entity = _storage.GetAll<AuctionEntity>().Single();
            entity.Name.Should().Be("Harvest Home");
            entity.AuctionDate.Should().Be(_request.Input.AuctionDate);
        }

        [Fact]
        public async Task GivenInvalidAuctionToCreateWhenExecuteThenAuctionIsNotSaved()
        {
            _request.Input.Name = "";
            
            var result = await _useCase.ExecuteAsync(_request);
            result.Output.IsValid.Should().BeFalse();
            _storage.GetAll<AuctionEntity>().Should().BeEmpty();
        }
    }
}