using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Common.Gateways;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Validation
{
    public class UpdateAuctionItemCommandValidator : AbstractValidator<UpdateAuctionItemCommand>
    {
        public UpdateAuctionItemCommandValidator(AggregateGateway<Auction, AuctionId> gateway)
        {
            RuleFor(c => c.OldItemName).Required();
            RuleFor(c => c.AuctionId).MustExistAsync(gateway);
            RuleFor(c => c.Quantity).GreaterThan(0);
        }
    }
}