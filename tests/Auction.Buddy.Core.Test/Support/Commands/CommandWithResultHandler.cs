using System.Threading.Tasks;
using Auction.Buddy.Core.Common.Commands;

namespace Auction.Buddy.Core.Test.Support.Commands
{
    public class CommandWithResult
    {
        public object Result { get; }

        public CommandWithResult(object result)
        {
            Result = result;
        }
    }

    public class CommandWithResultHandler : CommandHandler<CommandWithResult, object>
    {
        public Task<CommandResult<object>> HandleAsync(CommandWithResult command)
        {
            return Task.FromResult(new CommandResult<object>(command.Result));
        }
    }
}