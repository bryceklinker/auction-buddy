using System;
using Auction.Buddy.Core.Auctions.Dtos;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Validators
{
    public class CreateAuctionDtoValidator : AbstractValidator<CreateAuctionDto>
    {
        public CreateAuctionDtoValidator()
        {
            RuleFor(d => d.AuctionDate).Must(r => DateTime.TryParse(r, out _));
            RuleFor(d => d.Name).NotNull().NotEmpty();
        }
    }
}