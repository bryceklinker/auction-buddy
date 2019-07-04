using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    [Binding]
    public class Hooks
    {
        private static readonly Lazy<FakeIdentityServerApplication> LazyIdentityApplication = 
            new Lazy<FakeIdentityServerApplication>(() => new FakeIdentityServerApplication());

        private static readonly Lazy<AuctionBuddyWebApplication> LazyAuctionBuddyApplication =
            new Lazy<AuctionBuddyWebApplication>(() => new AuctionBuddyWebApplication());

        private static FakeIdentityServerApplication IdentityApplication => LazyIdentityApplication.Value;
        private static AuctionBuddyWebApplication AuctionBuddyApplication => LazyAuctionBuddyApplication.Value;
        
        [BeforeScenario]
        public async Task BeforeScenario()
        {
            await IdentityApplication.StartAsync();
            await AuctionBuddyApplication.StartAsync();
        }

        [AfterTestRun]
        public static async Task AfterTestRun()
        {
            await IdentityApplication.StopAsync();
            await AuctionBuddyApplication.StopAsync();
        }
    }
}