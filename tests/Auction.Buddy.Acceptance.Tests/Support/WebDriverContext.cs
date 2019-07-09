using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
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
                BinaryLocation = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "chromedriver.exe"))
                    : null
            };
            options.AddArgument("--headless");
            options.AddArgument("--verbose");
            options.AddArgument($"--log-path={Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "chromedriver.log"))}");
            return options;
        }
    }
}