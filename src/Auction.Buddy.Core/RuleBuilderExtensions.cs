using Auction.Buddy.Core.Common;
using Auction.Buddy.Core.Common.Storage;
using FluentValidation;

namespace Auction.Buddy.Core
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        {
            return builder.NotNull().NotEmpty();
        }

        public static IRuleBuilderOptions<T, TId> MustExistAsync<T, TAggregate, TId>(
            this IRuleBuilder<T, TId> builder,
            EventStore eventStore) 
            where TAggregate : AggregateRoot<TId> 
            where TId : Identity
        {
            return builder.NotNull()
                .MustAsync(async (id, token) => await eventStore.LoadAggregateAsync<TAggregate, TId>(id) != null);
        }
    }
}