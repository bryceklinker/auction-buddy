using Auction.Buddy.Core.Auctions.Commands;
using Auction.Buddy.Core.Common.Storage;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Validation
{
    public class UpdateAuctionItemCommandValidator : AbstractValidator<UpdateAuctionItemCommand>
    {
        public UpdateAuctionItemCommandValidator(EventStore eventStore)
        {
            RuleFor(c => c.OldItemName).Required();
            RuleFor(c => c.AuctionId).MustExistAsync<UpdateAuctionItemCommand, Auction, AuctionId>(eventStore);
            RuleFor(c => c.Quantity).GreaterThan(0);
        }
    }
}