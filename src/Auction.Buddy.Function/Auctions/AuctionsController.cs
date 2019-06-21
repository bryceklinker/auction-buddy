using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation.Results;
using Harvest.Home.Core.Auctions.Create;
using Harvest.Home.Core.Common.Serialization;
using Harvest.Home.Core.Common.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace Auction.Buddy.Function.Auctions
{
    public class AuctionsController
    {
        private readonly IAuctionBuddySerializer _serializer;
        private readonly IUseCase<CreateAuctionDto, ValidationResult> _createAuctionUseCase;

        public AuctionsController(IUseCase<CreateAuctionDto, ValidationResult> createAuctionUseCase, IAuctionBuddySerializer serializer)
        {
            _createAuctionUseCase = createAuctionUseCase;
            _serializer = serializer;
        }

        [FunctionName("CreateAuction")]
        public async Task<IActionResult> CreateAuction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auctions")]
            HttpRequestMessage request)
        {
            var dto = await _serializer.DeserializeAsync<CreateAuctionDto>(request);
            var response = await _createAuctionUseCase.ExecuteAsync(new UseCaseRequest<CreateAuctionDto, ValidationResult>(dto));
            return response.ToActionResult();
        }
    }
}