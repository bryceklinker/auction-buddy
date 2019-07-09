using System.Threading.Tasks;
using Auction.Buddy.Acceptance.Tests.Support;
using Auction.Buddy.Acceptance.Tests.Support.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Auction.Buddy.Acceptance.Tests.Features.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;
        private readonly AuctionsPage _auctionsPage;
        private string _username;
        private string _password;

        public LoginSteps(WebDriverContext context)
        {
            _loginPage = new LoginPage(context);
            _auctionsPage = new AuctionsPage(context);
        }

        [Given("I have invalid credentials")]
        public void GivenIHaveInvalidCredentials()
        {
            _username = "invalid";
            _password = "invalid";
        }

        [Given("I have valid credentials")]
        public void GivenIHaveValidCredentials()
        {
            _username = Credentials.AdminCredentials.Username;
            _password = Credentials.AdminCredentials.Password;
        }

        [Given("I have logged in")]
        public async Task GivenIHaveLoggedIn()
        {
            _loginPage.Login(Credentials.AdminCredentials.Username, Credentials.AdminCredentials.Password);
            await Eventually.Do(() => _auctionsPage.IsListVisible().Should().BeTrue());
        }

        [When("I login")]
        public void WhenILogin()
        {
            _loginPage.Login(_username, _password);
        }

        [Then("I should see invalid credentials")]
        public async Task ThenIShouldSeeInvalidCredentials()
        {
            await Eventually.Do(() => _loginPage.GetErrorText().Should().Contain("Invalid"));
        }

        [Then("I should see auctions")]
        public async Task ThenIShouldSeeAuctions()
        {
            await Eventually.Do(() => _auctionsPage.IsListVisible().Should().BeTrue());

        }
    }
}