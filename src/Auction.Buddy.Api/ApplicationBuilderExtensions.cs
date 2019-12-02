using System.Net;
using Auction.Buddy.Persistence.Common.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Buddy.Api
{
    public static class ApplicationBuilderExtensions
    {
        public static void MigrateDatabases(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            MigrateDatabase<EntityFrameworkEventPersistence>(scope);
            MigrateDatabase<EntityFrameworkReadStore>(scope);
        }

        private static void MigrateDatabase<TContext>(IServiceScope scope)
            where TContext : DbContext
        {
            using var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Database.Migrate();
        }
    }
}