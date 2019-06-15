using System;
using Microsoft.Extensions.Configuration;
using Xamarin.UITest;

namespace Auction.Buddy.Mobile.Device.Tests.Support.Pages
{
    public abstract class PageBase
    {
        protected IApp App => AppManager.Instance.App;
        protected IConfiguration Config => AppManager.Instance.Configuration;
    }
}
