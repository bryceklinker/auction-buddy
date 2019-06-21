using System;
using System.Linq;
using Xamarin.UITest.Queries;

namespace Auction.Buddy.Mobile.Device.Tests.Support.Pages
{
    public class AuctionDetailPage : PageBase
    {
        private static Func<AppQuery, AppQuery> Page => a => a.Marked("auction-detail");
        private static Func<AppQuery, AppQuery> AuctionDate => a => a.Marked("auction-date");
        private static Func<AppQuery, AppQuery> AuctionName => a => a.Marked("auction-name");

        public bool IsDisplayed()
        {
            return App.Query(Page).Any();
        }

        public string GetAuctionDate()
        {
            return App.Query(AuctionDate).First().Text;
        }

        public string GetAuctionName()
        {
            return App.Query(AuctionName).First().Text;
        }
    }
}
