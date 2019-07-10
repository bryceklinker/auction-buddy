using System.Linq;

namespace Auction.Buddy.Acceptance.Tests.Support.Pages
{
    public class AuctionsPage : Page
    {
        public AuctionsPage(WebDriverContext context) 
            : base(context)
        {
        }

        public void Navigate()
        {
            Driver.Navigate().GoToUrl($"{BaseUrl}/auctions");
        }

        public bool IsListVisible()
        {
            return Driver.FindElementsByTestId("auction-list").Any();
        }

        public void GoToCreateAuction()
        {
            Driver.FindElementByTestId("create-auction-button").Click();
        }

        public int GetAuctionsCount()
        {
            return Driver.FindElementsByTestId("auction-item").Count;
        }
    }
}