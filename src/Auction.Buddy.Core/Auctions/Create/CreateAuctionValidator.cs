using System;
using FluentValidation;

namespace Harvest.Home.Core.Auctions.Create
{
    public class CreateAuctionValidator : AbstractValidator<CreateAuctionDto>
    {
        public CreateAuctionValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.AuctionDate).GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Date.AddDays(-1));
        }   
    }
}