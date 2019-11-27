using System;
using Auction.Buddy.Core.Auctions.Commands;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Validation
{
    public class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
    {
        public CreateAuctionCommandValidator()
        {
            RuleFor(c => c.Name).Required();

            RuleFor(c => c.AuctionDate)
                .GreaterThanOrEqualTo(new DateTimeOffset(DateTime.Today));
        }
    }
}