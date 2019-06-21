using AutoMapper;

namespace Harvest.Home.Core.Tests.Support
{
    public static class MapperFactory
    {
        public static IMapper Create()
        {
            return new MapperConfiguration(c => c.AddMaps(typeof(ServiceCollectionExtensions)))
                .CreateMapper();
        }
    }
}