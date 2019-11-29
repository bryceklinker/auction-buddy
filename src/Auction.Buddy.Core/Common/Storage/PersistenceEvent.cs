using System;
using Newtonsoft.Json;

namespace Auction.Buddy.Core.Common.Storage
{
    public class PersistenceEvent
    {
        public DateTimeOffset Timestamp { get; set; }
        public string AggregateId { get; set; }
        public string EventName { get; set; }
        public string SerializedEvent { get; set; }
    }
}