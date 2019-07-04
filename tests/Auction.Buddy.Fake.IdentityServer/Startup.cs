using Auction.Buddy.Fake.IdentityServer.ApiResources;
using Auction.Buddy.Fake.IdentityServer.Clients;
using Auction.Buddy.Fake.IdentityServer.Resources;
using Auction.Buddy.Fake.IdentityServer.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Buddy.Fake.IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryApiResources(ApiResourcesFactory.Create())
                .AddInMemoryClients(ClientsFactory.Create())
                .AddTestUsers(TestUsersFactory.Create())
                .AddInMemoryIdentityResources(IdentityResourcesFactory.Create());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}