using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Chrome;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public class WebDriverContext : IDisposable
    {
        public string BaseUrl { get; }
        public ChromeDriver Driver { get; }
        public WebDriverContext()
        {
            BaseUrl = "https://localhost:5001";
            Driver = CreateChromeDriver();
        }


        public void Dispose()
        {
            Driver.Dispose();
        }

        public void Reset()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
        }

        private ChromeDriver CreateChromeDriver()
        {
            return new ChromeDriver(CreateChromeOptions())
            {
                Url = BaseUrl
            };
        }

        private static ChromeOptions CreateChromeOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            return options;
        }
    }
}