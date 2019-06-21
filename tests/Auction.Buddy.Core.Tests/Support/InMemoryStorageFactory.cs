using System;
using Harvest.Home.Core.Common.Storage;
using Microsoft.EntityFrameworkCore;

namespace Harvest.Home.Core.Tests.Support
{
    public static class InMemoryStorageFactory
    {
        public static IStorage Create()
        {
            var options = new DbContextOptionsBuilder<StorageContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options;
            var context = new StorageContext(options);
            return new Storage(context);
        }
    }
}