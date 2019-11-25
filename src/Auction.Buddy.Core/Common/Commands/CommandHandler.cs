using System.Threading.Tasks;

namespace Auction.Buddy.Core.Common.Commands
{
    public interface CommandHandler<in TCommand>
    {
        Task<CommandResult> HandleAsync(TCommand command);
    }

    public interface CommandHandler<in TCommand, TResult>
    {
        Task<CommandResult<TResult>> HandleAsync(TCommand command);
    }
}