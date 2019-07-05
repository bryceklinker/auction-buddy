using OpenQA.Selenium;

namespace Auction.Buddy.Acceptance.Tests.Support.Pages
{
    public class LoginPage : Page
    {
        public LoginPage(WebDriverContext context) 
            : base(context)
        {
        }

        public void Login(string username, string password)
        {
            Driver.FindElementByTestId("username-input").SendKeys(username);
            Driver.FindElementByTestId("password-input").SendKeys(password);
            Driver.FindElementByTestId("login-button").Click();
        }

        public string GetErrorText()
        {
            return Driver.FindElementByTestId("login-error").Text;
        }
    }
}