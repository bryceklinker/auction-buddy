using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class RemoteWebDriverExtensions
    {
        public static IWebElement FindElementByTestId(this RemoteWebDriver driver, string testId)
        {
            return driver.FindElement(By.CssSelector($"[data-testid='{testId}']"));
        }

        public static IReadOnlyCollection<IWebElement> FindElementsByTestId(this RemoteWebDriver driver, string testId)
        {
            return driver.FindElements(By.CssSelector($"[data-testid='{testId}']"));
        }

        public static async Task WaitForElementByTestId(
            this RemoteWebDriver driver, 
            string testId, 
            int waitMilliseconds = Timeouts.DefaultWaitMilliseconds,
            int delayMilliseconds = Timeouts.DefaultDelayMilliseconds)
        {
            var endWaitingTime = DateTime.UtcNow.AddMilliseconds(waitMilliseconds);
            while (endWaitingTime >= DateTime.UtcNow)
            {
                if (driver.ElementExists(testId))
                    return;

                await Task.Delay(delayMilliseconds);
            }
        }

        private static bool ElementExists(this RemoteWebDriver driver, string testId)
        {
            return driver.FindElementsByTestId(testId).Any();
        }
    }
}