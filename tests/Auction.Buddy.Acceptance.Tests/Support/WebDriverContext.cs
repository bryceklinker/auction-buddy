using System;
using OpenQA.Selenium.Chrome;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public class WebDriverContext : IDisposable
    {
        public ChromeDriver Driver { get; }
        public WebDriverContext()
        {
            Driver = new ChromeDriver
            {
                Url = "https://localhost:5001"
            };
        }

        public void Dispose()
        {
            Driver.Dispose();
        }

        public void Reset()
        {
            Driver.Navigate().GoToUrl("https://localhost:5001");
        }
    }
}