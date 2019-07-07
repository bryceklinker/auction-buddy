using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Auction.Buddy.Acceptance.Tests.Support;
using Auction.Buddy.Acceptance.Tests.Support.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Auction.Buddy.Acceptance.Tests.Features.Steps
{
    [Binding]
    public class AuctionsSteps
    {
        private readonly AuctionsPage _auctionsPage;
        private readonly AuctionDetailPage _auctionDetailPage;
        private readonly CreateAuctionPage _createAuctionPage;

        private string _auctionName;
        private DateTime _auctionDate;

        public AuctionsSteps(WebDriverContext context)
        {
            _auctionsPage = new AuctionsPage(context);
            _auctionDetailPage = new AuctionDetailPage(context);
            _createAuctionPage = new CreateAuctionPage(context);
        }

        [When("I create an auction")]
        public async Task WhenICreateAnAuction()
        {
            _auctionName = "Harvest Home";
            _auctionDate = DateTime.Today.AddDays(45);
            
            _auctionsPage.GoToCreateAuction();
            await _createAuctionPage.CreateAuction(_auctionName, _auctionDate);
        }

        [When("I create an invalid auction")]
        public async Task WhenICreateAnInvalidAuction()
        {
            _auctionsPage.GoToCreateAuction();
            await _createAuctionPage.CreateAuction("", DateTime.MinValue);
        }

        [Then("I should see the new auction")]
        public async Task ThenIShouldSeeTheNewAuction()
        {
            await _auctionDetailPage.WaitToBeVisible();
            _auctionDetailPage.GetAuctionDate().Should().Contain(_auctionDate.ToString("MM/dd/yyyy"));
            _auctionDetailPage.GetAuctionName().Should().Contain(_auctionName);
        }

        [Then("I should see validation errors")]
        public async Task ThenIShouldSeeValidationErrors()
        {
            await Eventually.Do(() => _createAuctionPage.HasValidationErrors().Should().BeTrue());
        }
    }
}