using Auction.Buddy.Core.Common;

namespace Auction.Buddy.Core.Auctions
{
    public class AuctionItem : ValueObject
    {
        public string Name { get; }
        public string Description { get; }
        public int Quantity { get; }
        public string Donor { get; }

        public AuctionItem(string name, string donor, string description = null, int quantity = 1)
        {
            Name = name;
            Donor = donor;
            Description = description;
            Quantity = quantity;
        }
    }
}