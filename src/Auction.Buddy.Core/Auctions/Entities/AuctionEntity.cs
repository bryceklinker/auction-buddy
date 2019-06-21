using System;
using Harvest.Home.Core.Common.Storage;

namespace Harvest.Home.Core.Auctions.Entities
{
    public class AuctionEntity : Entity
    {
        public string Name { get; set; }
        public DateTimeOffset AuctionDate { get; set; }
    }
}