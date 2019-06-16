using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xamarin.UITest;

namespace Auction.Buddy.Mobile.Device.Tests.Support
{
    public class AuctionsContext
    {
        private const string BaseUrl = "http://localhost:7071";
        private readonly HttpClient _client;

        public IApp App => AppManager.Instance.App;

        public AuctionsContext()
        {
            _client = new HttpClient();
        }

        public async Task AddAuction(DateTime auctionDate)
        {
            await PostAsync("/auctions", new { auctionDate });
        }

        private async Task PostAsync(string path, object data)
        {
            var response = await _client.PostAsJsonAsync($"{BaseUrl}{path}", data);
            response.IsSuccessStatusCode.Should().BeTrue($"POST to {BaseUrl}{path} should respond successfully, but responded with {response.StatusCode}");
        }
    }
}