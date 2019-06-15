using System;
using System.Linq;
using Xamarin.UITest.Queries;

namespace Auction.Buddy.Mobile.Device.Tests.Support.Pages
{
    public class AuctionDetailPage : PageBase
    {
        private Func<AppQuery, AppQuery> Page => a => a.Marked("auction-detail");
        private Func<AppQuery, AppQuery> AuctionDate => a => a.Marked("auction-date");

        public bool IsDisplayed()
        {
            return App.Query(Page).Any();
        }

        public string GetAuctionDate()
        {
            return App.Query(AuctionDate).First().Text;
        }
    }
}
