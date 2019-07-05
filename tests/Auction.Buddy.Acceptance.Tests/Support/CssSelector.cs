using OpenQA.Selenium;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class CssSelector
    {
        public static By TestId(string value = null)
        {
            return By.CssSelector($"[data-testid='{value}']");
        }
    }
}