using System.Linq;
using FluentValidation.Results;

namespace Auction.Buddy.Core
{
    public static class ValidationResultExtensions
    {
        public static bool HasErrors(this ValidationResult result)
        {
            return result.Errors.Any();
        }
    }
}