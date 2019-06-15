using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace Auction.Buddy.Mobile.Device.Tests.Support
{
    public class AuctionsContext
    {
        private const string BaseUrl = "https://localhost:7071";
        private readonly HttpClient _client;

        public IApp App => AppManager.Instance.App;

        public AuctionsContext()
        {
            _client = new HttpClient();
        }

        public async Task AddAuction(DateTime auctionDate)
        {
            var response = await _client.PostAsJsonAsync($"{BaseUrl}/auctions", new
            {
                auctionDate
            });

            response.EnsureSuccessStatusCode();
        }
    }
}
