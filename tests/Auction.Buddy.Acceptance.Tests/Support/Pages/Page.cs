using System.Diagnostics;
using OpenQA.Selenium.Chrome;

namespace Auction.Buddy.Acceptance.Tests.Support.Pages
{
    public abstract class Page
    {
        private readonly WebDriverContext _context;

        protected ChromeDriver Driver => _context.Driver;
        protected string BaseUrl => _context.BaseUrl;
        
        protected Page(WebDriverContext context)
        {
            _context = context;
        }

        protected void Navigate(string path)
        {
            Driver.Navigate().GoToUrl($"{BaseUrl}{path}");
        }
    }
}