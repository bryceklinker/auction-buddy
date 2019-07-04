using System;
using Microsoft.Extensions.Configuration;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class TestSettings
    {
        private static readonly Lazy<IConfiguration> ConfigInstance = new Lazy<IConfiguration>(CreateConfiguration);

        private static IConfiguration Settings => ConfigInstance.Value;

        public static string Username => Settings["Username"];
        public static string Password => Settings["Password"];

        private static IConfiguration CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}