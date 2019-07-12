using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Buddy.Acceptance.Tests.Support.Pages
{
    public class CreateAuctionPage : Page
    {
        public CreateAuctionPage(WebDriverContext context) 
            : base(context)
        {
        }

        public void Navigate()
        {
            Navigate("/create-auction");
        }

        public async Task CreateAuction(string name, DateTime auctionDate)
        {
            await Driver.WaitForElementByTestId("create-auction");
            Driver.FindElementByTestId("create-auction-name-input").SendKeys(name);
            Driver.FindElementByTestId("create-auction-date-input").SendKeys(auctionDate.ToString("MM/dd/yyyy"));
            Driver.FindElementByTestId("create-auction-save-button").Click();
        }

        public bool HasValidationErrors()
        {
            return Driver.FindElementsByTestId("validation-errors").Any();
        }
    }
}