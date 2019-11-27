using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Gateways;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Commands
{
    public class RemoveAuctionItemCommand
    {
        public AuctionId AuctionId { get; }
        public string ItemName { get; }

        public RemoveAuctionItemCommand(AuctionId auctionId, string itemName)
        {
            AuctionId = auctionId;
            ItemName = itemName;
        }
    }

    public class RemoveAuctionItemCommandHandler : CommandHandler<RemoveAuctionItemCommand>
    {
        private readonly AggregateGateway<Auction, AuctionId> _gateway;
        private readonly IValidator<RemoveAuctionItemCommand> _validator;

        public RemoveAuctionItemCommandHandler(AggregateGateway<Auction,AuctionId> gateway)
        {
            _gateway = gateway;
            _validator = new RemoveAuctionItemCommandValidator(gateway);
        }

        public async Task<CommandResult> HandleAsync(RemoveAuctionItemCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (validationResult.HasErrors())
                return new CommandResult(validationResult);
            
            var auction = await _gateway.FindByIdAsync(command.AuctionId);
            auction.RemoveAuctionItem(command.ItemName);
            await _gateway.CommitAsync(auction);
            return new CommandResult();
        }
    }
}