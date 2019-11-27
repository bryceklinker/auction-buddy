using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Common.Gateways;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Validation
{
    public class AddAuctionItemCommandValidator : AbstractValidator<AddAuctionItemCommand>
    {
        public AddAuctionItemCommandValidator(AggregateGateway<Auction, AuctionId> gateway)
        {
            RuleFor(c => c.AuctionItem)
                .NotNull()
                .SetValidator(new AuctionItemValidator());

            RuleFor(c => c.AuctionId)
                .NotNull()
                .MustAsync(async (id, token) => await gateway.FindByIdAsync(id) != null);
        }
    }
}