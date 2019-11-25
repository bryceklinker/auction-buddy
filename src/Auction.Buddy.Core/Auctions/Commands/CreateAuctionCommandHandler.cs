using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Gateways;

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
    
    public class CreateAuctionCommandHandler : CommandHandler<CreateAuctionCommand>
    {
        private readonly AggregateGateway<Auction, AuctionId> _gateway;
        private readonly AuctionFactory _auctionFactory;

        public CreateAuctionCommandHandler(AggregateGateway<Auction,AuctionId> gateway)
        {
            _gateway = gateway;
            _auctionFactory = new AuctionAggregateFactory();
        }

        public async Task<CommandResult> HandleAsync(CreateAuctionCommand command)
        {
            await _gateway.CommitAsync(_auctionFactory.Create(command.Name, command.AuctionDate));
            return new CommandResult();
        }
    }
}