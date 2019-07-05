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
            Driver.FindElement(CssSelector.TestId("username-input")).SendKeys(username);
            Driver.FindElement(CssSelector.TestId("password-input")).SendKeys(password);
            Driver.FindElement(CssSelector.TestId("login-button")).Click();
        }

        public string GetErrorText()
        {
            return Driver.FindElement(CssSelector.TestId("login-error")).Text;
        }
    }
}