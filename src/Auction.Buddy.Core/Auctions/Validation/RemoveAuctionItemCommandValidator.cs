using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Common.Gateways;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Validation
{
    public class RemoveAuctionItemCommandValidator : AbstractValidator<RemoveAuctionItemCommand>
    {
        public RemoveAuctionItemCommandValidator(AggregateGateway<Auction, AuctionId> gateway)
        {
            RuleFor(c => c.ItemName).Required();
            RuleFor(c => c.AuctionId).MustExistAsync(gateway);
        }
    }
}