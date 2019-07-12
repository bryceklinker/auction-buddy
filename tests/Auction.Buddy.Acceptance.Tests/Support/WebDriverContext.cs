using System;
using System.Collections.Concurrent;
using Auction.Buddy.Acceptance.Tests.Support.Pages;
using OpenQA.Selenium.Chrome;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public class WebDriverContext : IDisposable
    {
        private readonly ConcurrentDictionary<Type, Page> _pages;
        
        public string BaseUrl { get; }
        public ChromeDriver Driver { get; }
        
        public WebDriverContext()
        {
            BaseUrl = "https://localhost:5001";
            Driver = CreateChromeDriver();
            _pages = new ConcurrentDictionary<Type, Page>();
        }

        public T GetPage<T>()
            where T : Page
        {
            return (T) _pages.GetOrAdd(typeof(T), t => CreatePage<T>());
        }

        public void Dispose()
        {
            Driver.Dispose();
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

        private T CreatePage<T>()
            where T : Page
        {
            if (typeof(T) == typeof(AuctionsPage))
                return new AuctionsPage(this) as T;

            if (typeof(T) == typeof(CreateAuctionPage))
                return new CreateAuctionPage(this) as T;

            if (typeof(T) == typeof(AuctionDetailPage))
                return new AuctionDetailPage(this) as T;

            if (typeof(T) == typeof(LoginPage))
                return new LoginPage(this) as T;
            
            throw new InvalidOperationException($"Page of type {typeof(T)} was not recognized.");
        }
    }
}