using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.Buddy.Core.Auctions.Validation;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Gateways;
using FluentValidation;
using FluentValidation.Results;

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
        private readonly AggregateGateway<Auction, AuctionId> _gateway;
        private readonly AuctionFactory _auctionFactory;
        private readonly IValidator<CreateAuctionCommand> _validator;

        public CreateAuctionCommandHandler(AggregateGateway<Auction,AuctionId> gateway)
        {
            _gateway = gateway;
            _auctionFactory = new AuctionAggregateFactory();
            _validator = new CreateAuctionCommandValidator();
        }

        public async Task<CommandResult<AuctionId>> HandleAsync(CreateAuctionCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (validationResult.HasErrors())
                return new CommandResult<AuctionId>(validationResult);
            
            var auction = _auctionFactory.Create(command.Name, command.AuctionDate);
            await _gateway.CommitAsync(auction);
            return new CommandResult<AuctionId>(auction.Id);
        }
    }
}