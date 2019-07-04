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
            Driver.FindElement(By.Id("usernameInput")).SendKeys(username);
            Driver.FindElement(By.Id("passwordInput")).SendKeys(password);
            Driver.FindElement(By.Id("loginButton")).Click();
        }
    }
}