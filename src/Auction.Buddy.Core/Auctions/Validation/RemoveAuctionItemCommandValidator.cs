using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Common.Storage;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Validation
{
    public class RemoveAuctionItemCommandValidator : AbstractValidator<RemoveAuctionItemCommand>
    {
        public RemoveAuctionItemCommandValidator(EventStore eventStore)
        {
            RuleFor(c => c.ItemName).Required();
            RuleFor(c => c.AuctionId).MustExistAsync<RemoveAuctionItemCommand, Auction, AuctionId>(eventStore);
        }
    }
}