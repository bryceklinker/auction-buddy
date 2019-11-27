using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Gateways;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Commands
{
    public class AddAuctionItemCommand
    {
        public AuctionId AuctionId { get; }
        public AuctionItem AuctionItem { get; }

        public AddAuctionItemCommand(AuctionId auctionId, AuctionItem auctionItem)
        {
            AuctionId = auctionId;
            AuctionItem = auctionItem;
        }
    }

    public class AddAuctionItemCommandHandler : CommandHandler<AddAuctionItemCommand>
    {
        private readonly AggregateGateway<Auction, AuctionId> _gateway;
        private readonly IValidator<AddAuctionItemCommand> _validator;

        public AddAuctionItemCommandHandler(AggregateGateway<Auction, AuctionId> gateway)
        {
            _gateway = gateway;
            _validator = new AddAuctionItemCommandValidator(gateway);
        }

        public async Task<CommandResult> HandleAsync(AddAuctionItemCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (validationResult.HasErrors())
                return new CommandResult(validationResult);
            
            var auction = await _gateway.FindByIdAsync(command.AuctionId);
            auction.AddAuctionItem(command.AuctionItem);
            await _gateway.CommitAsync(auction);
            return new CommandResult();
        }
    }
}