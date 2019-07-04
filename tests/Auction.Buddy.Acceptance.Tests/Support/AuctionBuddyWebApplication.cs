namespace Auction.Buddy.Acceptance.Tests.Support
{
    public class AuctionBuddyWebApplication : WebApplication
    {
        public AuctionBuddyWebApplication() 
            : base("Auction Buddy", SolutionPaths.WebDirectory, 5001)
        {
        }
    }
}