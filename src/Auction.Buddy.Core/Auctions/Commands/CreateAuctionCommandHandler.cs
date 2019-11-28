using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Storage;
using FluentValidation;

namespace Auction.Buddy.Core.Auctions.Commands
{
    public class CreateAuctionCommand
    {
        public string Name { get; }
        public DateTimeOffset AuctionDate { get; }

        public CreateAuctionCommand(string name, in DateTimeOffset auctionDate)
        {
            Name = name;
            AuctionDate = auctionDate;
        }
    }
    
    public class CreateAuctionCommandHandler : CommandHandler<CreateAuctionCommand, AuctionId>
    {
        private readonly EventStore _eventStore;
        private readonly AuctionFactory _auctionFactory;
        private readonly IValidator<CreateAuctionCommand> _validator;

        public CreateAuctionCommandHandler(EventStore eventStore)
        {
            _eventStore = eventStore;
            _auctionFactory = new AuctionAggregateFactory();
            _validator = new CreateAuctionCommandValidator();
        }

        public async Task<CommandResult<AuctionId>> HandleAsync(CreateAuctionCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (validationResult.HasErrors())
                return new CommandResult<AuctionId>(validationResult);
            
            var auction = _auctionFactory.Create(command.Name, command.AuctionDate);
            await auction.CommitAsync(_eventStore);
            return new CommandResult<AuctionId>(auction.Id);
        }
    }
}