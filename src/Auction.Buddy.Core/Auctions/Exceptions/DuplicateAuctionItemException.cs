using System;

namespace Auction.Buddy.Core.Auctions.Exceptions
{
    public class DuplicateAuctionItemException : Exception
    {
        public DuplicateAuctionItemException(AuctionId id, string name)
            : base($"Auction {id} already has an item with name {name}.")
        {
            
        }
    }
}