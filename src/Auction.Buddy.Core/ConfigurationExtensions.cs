using Microsoft.Extensions.Configuration;

namespace Harvest.Home.Core
{
    public static class ConfigurationExtensions
    {
        public static string GetUseInMemoryDatabase(this IConfiguration configuration)
        {
            return configuration["USE_IN_MEMORY_DATABASE"];
        }
    }
}