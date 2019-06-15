using NUnit.Framework;
using Xamarin.UITest;

namespace Auction.Buddy.Mobile.Device.Tests.Support
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public abstract class FeatureBase
    {
        protected FeatureBase(Platform platform)
        {
            AppManager.Instance.SetPlatform(platform);
        }
    }
}
