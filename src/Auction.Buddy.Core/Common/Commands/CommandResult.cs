using FluentValidation.Results;

namespace Auction.Buddy.Core.Common.Commands
{
    public class CommandResult
    {
        public bool WasSuccessful => ValidationResult == null || ValidationResult.IsValid;
        public ValidationResult ValidationResult { get; }

        public CommandResult()
        {
            ValidationResult = null;
        }
        
        public CommandResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }

    public class CommandResult<TResult> : CommandResult
    {
        public TResult Result { get; }
        
        public CommandResult(TResult result)
        {
            Result = result;
        }

        public CommandResult(ValidationResult validationResult)
            : base(validationResult)
        {
            Result = default;
        }
    }
}