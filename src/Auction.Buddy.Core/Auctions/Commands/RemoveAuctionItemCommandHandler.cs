using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Storage;
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
        private readonly EventStore _eventStore;
        private readonly IValidator<RemoveAuctionItemCommand> _validator;

        public RemoveAuctionItemCommandHandler(EventStore eventStore)
        {
            _eventStore = eventStore;
            _validator = new RemoveAuctionItemCommandValidator(eventStore);
        }

        public async Task<CommandResult> HandleAsync(RemoveAuctionItemCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (validationResult.HasErrors())
                return new CommandResult(validationResult);

            var auction = await _eventStore.LoadAggregateAsync<Auction, AuctionId>(command.AuctionId);
            auction.RemoveAuctionItem(command.ItemName);
            await auction.CommitAsync(_eventStore);
            return new CommandResult();
        }
    }
}