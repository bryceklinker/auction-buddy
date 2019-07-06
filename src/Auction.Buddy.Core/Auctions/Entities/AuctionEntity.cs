using System;
using Auction.Buddy.Core.Auctions.Dtos;
using Auction.Buddy.Core.Common.Entities;

namespace Auction.Buddy.Core.Auctions.Entities
{
    public class AuctionEntity : Entity
    {
        public string Name { get; internal set; }
        public DateTime AuctionDate { get; internal set; }

        public AuctionEntity(string name, DateTime auctionDate)
        {
            Name = name;
            AuctionDate = auctionDate;
        }

        public AuctionEntity(int id, string name, DateTime auctionDate)
            : this(name, auctionDate)
        {
            Id = id;
        }
        
        public AuctionDto ToDto()
        {
            return new AuctionDto
            {
                Id = Id,
                Name = Name,
                AuctionDate = AuctionDate.ToString("yyyy-MM-dd")
            };
        }
    }
}