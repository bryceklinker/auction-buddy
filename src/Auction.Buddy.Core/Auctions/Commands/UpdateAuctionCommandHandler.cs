using System;
using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Commands;
using Auction.Buddy.Core.Common.Storage;

namespace Auction.Buddy.Core.Auctions.Commands
{
    public class UpdateAuctionCommand
    {
        public AuctionId AuctionId { get; }
        public string Name { get; }
        public DateTimeOffset? AuctionDate { get; }

        public UpdateAuctionCommand(AuctionId auctionId, string name, DateTimeOffset? auctionDate)
        {
            AuctionId = auctionId;
            Name = name;
            AuctionDate = auctionDate;
        }
    }

    public class UpdateAuctionCommandHandler : CommandHandler<UpdateAuctionCommand>
    {
        private readonly EventStore _eventStore;

        public UpdateAuctionCommandHandler(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<CommandResult> HandleAsync(UpdateAuctionCommand command)
        {
            var auction = await _eventStore.LoadAggregateAsync<Auction, AuctionId>(command.AuctionId);
            auction.UpdateAuction(command.Name, command.AuctionDate);
            await auction.CommitAsync(_eventStore);
            return new CommandResult();
        }
    }
}