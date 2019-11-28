using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Commands;

namespace Auction.Buddy.Core.Test.Support.Commands
{
    public class CommandWithoutResult
    {
        public bool WasHandled { get; private set; }

        public void Handle()
        {
            WasHandled = true;
        }
    }

    public class CommandWithoutResultHandler : CommandHandler<CommandWithoutResult>
    {
        public Task<CommandResult> HandleAsync(CommandWithoutResult command)
        {
            command.Handle();
            return Task.FromResult(new CommandResult());
        }
    }    
}