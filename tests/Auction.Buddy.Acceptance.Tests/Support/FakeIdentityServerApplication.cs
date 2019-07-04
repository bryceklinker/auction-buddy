namespace Auction.Buddy.Acceptance.Tests.Support
{
    public class FakeIdentityServerApplication : WebApplication
    {
        public FakeIdentityServerApplication() 
            : base("Identity Server", SolutionPaths.IdentityServerDirectory, 5002)
        {
        }
    }
}