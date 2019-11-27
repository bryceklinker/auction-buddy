using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Gateways;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Commands
{
    public class UpdateAuctionItemCommand
    {
        public AuctionId AuctionId { get; }
        public string OldItemName { get; }
        public string NewItemName { get; }
        public string Donor { get; }
        public string Description { get; }
        public int? Quantity { get; }

        public UpdateAuctionItemCommand(
            AuctionId auctionId,
            string oldItemName,
            string newItemName = null,
            string donor = null,
            string description = null,
            int? quantity = null)
        {
            AuctionId = auctionId;
            OldItemName = oldItemName;
            NewItemName = newItemName;
            Donor = donor;
            Description = description;
            Quantity = quantity;
        }
    }

    public class UpdateAuctionItemCommandHandler : CommandHandler<UpdateAuctionItemCommand>
    {
        private readonly AggregateGateway<Auction, AuctionId> _gateway;
        private readonly IValidator<UpdateAuctionItemCommand> _validator;

        public UpdateAuctionItemCommandHandler(AggregateGateway<Auction, AuctionId> gateway)
        {
            _gateway = gateway;
            _validator = new UpdateAuctionItemCommandValidator(_gateway);
        }

        public async Task<CommandResult> HandleAsync(UpdateAuctionItemCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (validationResult.HasErrors())
                return new CommandResult(validationResult);
            
            var auction = await _gateway.FindByIdAsync(command.AuctionId);
            auction.UpdateAuctionItem(
                command.OldItemName,
                command.NewItemName,
                command.Donor,
                command.Description,
                command.Quantity
            );

            await _gateway.CommitAsync(auction);
            return new CommandResult();
        }
    }
}