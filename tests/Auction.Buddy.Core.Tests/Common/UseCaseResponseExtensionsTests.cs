using System.Net;
using Auction.Buddy.Function;
using FluentAssertions;
using FluentValidation.Results;
using Harvest.Home.Core.Auctions.Create;
using Harvest.Home.Core.Common.UseCases;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Harvest.Home.Core.Tests.Common
{
    public class UseCaseResponseExtensionsTests
    {
        [Fact]
        public void GivenUseCaseResponseWithInvalidValidationResultWhenConvertedToActionResultThenShouldReturnBadRequest()
        {
            var validationResult = new ValidationResult(new []{new ValidationFailure("idk", "something bad")});
            var response = new UseCaseRequest<CreateAuctionDto, ValidationResult>(new CreateAuctionDto())
                .CreateResponse(validationResult);

            var result = response.ToActionResult();
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be(validationResult);
            result.As<BadRequestObjectResult>().StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
        }

        [Fact]
        public void GivenUseCaseResponseWithValidValidationResultWhenConvertedToActionResultThenShouldReturnOkRequest()
        {
            var validationResult = new ValidationResult();
            var response = new UseCaseRequest<CreateAuctionDto, ValidationResult>(new CreateAuctionDto())
                .CreateResponse(validationResult);

            var result = response.ToActionResult();
            result.Should().BeOfType<OkResult>();
        }
    }
}