using FluentValidation;

namespace Auction.Buddy.Core
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        {
            return builder.NotNull().NotEmpty();
        }
    }
}