using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Storage;
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
        private readonly EventStore _eventStore;
        private readonly IValidator<AddAuctionItemCommand> _validator;

        public AddAuctionItemCommandHandler(EventStore eventStore)
        {
            _eventStore = eventStore;
            _validator = new AddAuctionItemCommandValidator(eventStore);
        }

        public async Task<CommandResult> HandleAsync(AddAuctionItemCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (validationResult.HasErrors())
                return new CommandResult(validationResult);
            
            var auction = await _eventStore.LoadAggregateAsync<Auction, AuctionId>(command.AuctionId);
            auction.AddAuctionItem(command.AuctionItem);
            await auction.CommitAsync(_eventStore);
            return new CommandResult();
        }
    }
}