using System;
using Microsoft.Extensions.Configuration;
using Xamarin.UITest;

namespace Auction.Buddy.Mobile.Device.Tests.Support
{
    public class AppManager
    {
        private static readonly Lazy<AppManager> _managerInstance = new Lazy<AppManager>(() => new AppManager());

        public static AppManager Instance => _managerInstance.Value;

        public Platform Platform { get; private set; }
        public IApp App { get; private set; }
        public IConfiguration Configuration { get; }

        private AppManager()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(ApplicationPaths.AppSettingsPath)
                .AddEnvironmentVariables()
                .Build();
        }

        public void SetPlatform(Platform platform)
        {
            Platform = platform;
        }

        public IApp StartApp()
        {
            App = Platform == Platform.Android
                ? StartAndroidApp()
                : StartiOSApp();

            return App;
        }

        private IApp StartiOSApp()
        {
            return ConfigureApp.iOS
                .PreferIdeSettings()
                .AppBundle(ApplicationPaths.IOSAppPath)
                .StartApp();
        }

        private IApp StartAndroidApp()
        {
            return ConfigureApp.Android
                .PreferIdeSettings()
                .ApkFile(ApplicationPaths.AndroidApkPath)
                .StartApp();
        }
    }
}
