using OpenQA.Selenium.Chrome;

namespace Auction.Buddy.Acceptance.Tests.Support.Pages
{
    public abstract class Page
    {
        private readonly WebDriverContext _context;

        protected ChromeDriver Driver => _context.Driver;
        
        protected Page(WebDriverContext context)
        {
            _context = context;
        }
    }
}