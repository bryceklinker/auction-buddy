using System;

namespace Auction.Buddy.Core.Auctions.Exceptions
{
    public class MissingAuctionItemException : Exception
    {
        public MissingAuctionItemException(AuctionId auctionId, string name)
            : base($"Auction {auctionId} does not have an item with name {name}.")
        {
            
        }
    }
}