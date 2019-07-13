using System;

namespace Auction.Buddy.Core.Common
{
    public interface ISystemClock
    {
        DateTimeOffset UtcNow { get; }
    }

    public class SystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.Now;
    }
}