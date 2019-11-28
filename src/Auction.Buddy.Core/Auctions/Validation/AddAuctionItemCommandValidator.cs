using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Common.Storage;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Validation
{
    public class AddAuctionItemCommandValidator : AbstractValidator<AddAuctionItemCommand>
    {
        public AddAuctionItemCommandValidator(EventStore eventStore)
        {
            RuleFor(c => c.AuctionItem)
                .NotNull()
                .SetValidator(new AuctionItemValidator());

            RuleFor(c => c.AuctionId).MustExistAsync<AddAuctionItemCommand, Auction, AuctionId>(eventStore);
        }
    }
}