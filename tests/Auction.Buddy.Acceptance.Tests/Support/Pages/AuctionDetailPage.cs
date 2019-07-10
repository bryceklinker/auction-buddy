namespace Auction.Buddy.Acceptance.Tests.Support.Pages
{
    public class AuctionDetailPage : Page
    {
        public AuctionDetailPage(WebDriverContext context) 
            : base(context)
        {
        }

        public string GetAuctionName()
        {
            return Driver.FindElementByTestId("auction-name").Text;
        }

        public string GetAuctionDate()
        {
            return Driver.FindElementByTestId("auction-date").Text;
        }
    }
}