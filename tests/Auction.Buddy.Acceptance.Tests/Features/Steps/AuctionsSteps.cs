using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Auction.Buddy.Acceptance.Tests.Support;
using Auction.Buddy.Acceptance.Tests.Support.Pages;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

namespace Auction.Buddy.Acceptance.Tests.Features.Steps
{
    [Binding]
    public class AuctionsSteps
    {
        private readonly WebDriverContext _context;
        public AuctionsPage AuctionsPage => _context.GetPage<AuctionsPage>();
        public AuctionDetailPage AuctionDetailPage => _context.GetPage<AuctionDetailPage>();
        public CreateAuctionPage CreateAuctionPage => _context.GetPage<CreateAuctionPage>();

        public AuctionsSteps(WebDriverContext context)
        {
            _context = context;
        }

        [Given("auctions already exist")]
        public async Task GivenAuctionsAlreadyExist()
        {
            CreateAuctionPage.Navigate();
            await CreateAuctionPage.CreateAuctionAndWaitForDetails("One", DateTime.Today.AddDays(5));
            
            CreateAuctionPage.Navigate();
            await CreateAuctionPage.CreateAuctionAndWaitForDetails("Two", DateTime.Today.AddDays(5));
            
            CreateAuctionPage.Navigate();
            await CreateAuctionPage.CreateAuctionAndWaitForDetails("Three", DateTime.Today.AddDays(5));
        }

        [When("I create an auction")]
        public async Task WhenICreateAnAuction()
        {
            AuctionsPage.GoToCreateAuction();
            await CreateAuctionPage.CreateAuctionAndWaitForDetails("Harvest Home", DateTime.Today.AddDays(45));
        }

        [When("I create an invalid auction")]
        public async Task WhenICreateAnInvalidAuction()
        {
            AuctionsPage.GoToCreateAuction();
            await CreateAuctionPage.CreateAuction("", DateTime.MinValue);
        }

        [When("I view auctions")]
        public void WhenIViewAuctions()
        {
            AuctionsPage.Navigate();
        }

        [When("I select an auction")]
        public void WhenISelectAnAuction()
        {
            AuctionsPage.Navigate();
            AuctionsPage.SelectAuctionAtIndex(1);
        }

        [Then("I should see the new auction")]
        public async Task ThenIShouldSeeTheNewAuction()
        {
            await AssertAuctionDetails(CreateAuctionPage.LastAuctionDetails);
        }

        [Then("I should see validation errors")]
        public async Task ThenIShouldSeeValidationErrors()
        {
            await Eventually.Do(() => CreateAuctionPage.HasValidationErrors().Should().BeTrue());
        }

        [Then("I should see all auctions")]
        public async Task ThenIShouldSeeAllAuctions()
        {
            await Eventually.Do(() => AuctionsPage.GetAuctionsCount().Should().BeGreaterOrEqualTo(3));
        }

        [Then("I should see the auction's details")]
        public async Task ThenIShouldSeeTheAuctionsDetails()
        {
            await AssertAuctionDetails(CreateAuctionPage.CreatedAuctions.ElementAt(1));
        }

        private async Task AssertAuctionDetails(JToken auctionDetails)
        {
            await Eventually.Do(() =>
            {
                AuctionDetailPage.GetAuctionDate().Should().Contain(auctionDetails.Value<string>("auctionDate"));
                AuctionDetailPage.GetAuctionName().Should().Contain(auctionDetails.Value<string>("name"));
            });
        }
    }
}