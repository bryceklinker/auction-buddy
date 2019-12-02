using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.Buddy.Api.Common.Json;
using Auction.Buddy.Core;
using Auction.Buddy.Persistence;
using Auction.Buddy.Persistence.Common.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auction.Buddy.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        private string EventStoreConnectionString => _configuration.GetConnectionString("EventStore");
        private string ReadStoreConnectionString => _configuration.GetConnectionString("ReadStore");

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuctionBuddy()
                .AddAuctionBuddyEntityFramework(
                    eventStore => eventStore.UseSqlite(
                        EventStoreConnectionString, 
                        opts => opts.MigrationsAssembly(typeof(EntityFrameworkEventPersistence).Assembly.GetName().Name)
                    ),
                    readStore => readStore.UseSqlite(
                        ReadStoreConnectionString,
                        opts => opts.MigrationsAssembly(typeof(EntityFrameworkReadStore).Assembly.GetName().Name)
                    )
                )
                .AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.MigrateDatabases();
            app.UseHsts()
                .UseHttpsRedirection()
                .UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
