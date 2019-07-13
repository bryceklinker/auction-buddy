using System;
using Auction.Buddy.Core.Common;

namespace Auction.Buddy.Unit.Tests.Utilities
{
    public class StubSystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow { get; set; }
    }
}