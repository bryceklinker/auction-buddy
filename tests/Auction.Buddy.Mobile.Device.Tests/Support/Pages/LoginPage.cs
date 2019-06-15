using System;
using Xamarin.UITest.Queries;

namespace Auction.Buddy.Mobile.Device.Tests.Support.Pages
{
    public class LoginPage : PageBase
    {
        private Func<AppQuery, AppQuery> UsernameInput => a => a.Marked("username");
        private Func<AppQuery, AppQuery> PasswordInput => a => a.Marked("password");
        private Func<AppQuery, AppQuery> LoginButton => a => a.Marked("login-button");

        public void Login()
        {
            App.EnterText(UsernameInput, Config.Username());
            App.EnterText(PasswordInput, Config.Password());
            App.Tap(LoginButton);
        }
    }
}
