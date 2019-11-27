using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Validation
{
    public class AuctionItemValidator : AbstractValidator<AuctionItem>
    {
        public AuctionItemValidator()
        {
            RuleFor(i => i.Name).Required();
            RuleFor(i => i.Donor).Required();
            RuleFor(i => i.Quantity).GreaterThan(0);
        }
    }
}