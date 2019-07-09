using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Chrome;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public class WebDriverContext : IDisposable
    {
        public ChromeDriver Driver { get; }
        public WebDriverContext()
        {
            Driver = CreateChromeDriver();
        }


        public void Dispose()
        {
            Driver.Dispose();
        }

        public void Reset()
        {
            Driver.Navigate().GoToUrl("https://localhost:5001");
        }

        private static ChromeDriver CreateChromeDriver()
        {
            return new ChromeDriver(CreateChromeOptions())
            {
                Url = "https://localhost:5001"
            };
        }

        private static ChromeOptions CreateChromeOptions()
        {
            var options = new ChromeOptions
            {
                BinaryLocation = Environment.GetEnvironmentVariable("ChromeWebDriver")
            };
            options.AddArgument("--headless");
            return options;
        }
    }
}