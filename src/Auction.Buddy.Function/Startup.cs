using Auction.Buddy.Function;
using Harvest.Home.Core;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Auction.Buddy.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            
            builder.Services.AddAuctionBuddy(config);
        }
    }
}