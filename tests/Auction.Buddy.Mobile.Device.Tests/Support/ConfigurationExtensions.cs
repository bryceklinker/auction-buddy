using System;
using Microsoft.Extensions.Configuration;

namespace Auction.Buddy.Mobile.Device.Tests.Support
{
    public static class ConfigurationExtensions
    {
        public static string Username(this IConfiguration config)
        {
            return config["User:Username"];
        }

        public static string Password(this IConfiguration config)
        {
            return config["User:Password"];
        }
    }
}
