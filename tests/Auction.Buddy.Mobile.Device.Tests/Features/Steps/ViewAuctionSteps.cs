using System;
using System.Threading.Tasks;
using FluentAssertions;
using Auction.Buddy.Mobile.Device.Tests.Support;
using Auction.Buddy.Mobile.Device.Tests.Support.Pages;
using TechTalk.SpecFlow;

namespace Auction.Buddy.Mobile.Device.Tests.Features.Steps
{
    [Binding]
    public class ViewAuctionSteps
    {
        private readonly AuctionsContext _context;

        public ViewAuctionSteps(AuctionsContext context)
        {
            _context = context;
        }

        [Given("I have auctions")]
        public async Task GivenIHaveAnAuctionOnAsync()
        {
            await _context.AddAuction(new DateTime(2019, 02, 01));
            await _context.AddAuction(new DateTime(2019, 01, 01));
            await _context.AddAuction(new DateTime(2019, 03, 01));
        }

        [When("I log into the application")]
        public void WhenILogIntoTheApplication()
        {
            new LoginPage().Login();
        }

        [Then("I should see the current auction")]
        public void ThenIShouldSeeCurrentAuction()
        {
            var page = new AuctionDetailPage();
            page.IsDisplayed().Should().BeTrue();
            page.GetAuctionDate().Should().Contain("March 29, 2019");
        }
    }
}
