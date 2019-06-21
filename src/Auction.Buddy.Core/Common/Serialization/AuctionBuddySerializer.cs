using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Harvest.Home.Core.Common.Serialization
{
    public interface IAuctionBuddySerializer
    {
        Task<T> DeserializeAsync<T>(HttpRequestMessage request);
    }

    public class AuctionBuddySerializer : IAuctionBuddySerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            DateParseHandling = DateParseHandling.DateTimeOffset,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };
        
        public async Task<T> DeserializeAsync<T>(HttpRequestMessage request)
        {
            var json = await request.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }
    }
}