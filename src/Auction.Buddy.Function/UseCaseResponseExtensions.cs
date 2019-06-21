using FluentValidation.Results;
using Harvest.Home.Core.Common.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Buddy.Function
{
    public static class UseCaseResponseExtensions
    {
        public static IActionResult ToActionResult<TInput, TOutput>(this UseCaseResponse<TInput, TOutput> response)
        {
            var output = response.Output as ValidationResult;
            if (output != null && !output.IsValid)
                return new BadRequestObjectResult(output);
            return new OkResult();
        }
    }
}