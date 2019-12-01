namespace Auction.Buddy.Core.Auctions
{
    public class AuctionItemReadModel
    {
        public int Id { get; set; }
        public string AuctionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Donor { get; set; }
        public int Quantity { get; set; }
        
        public AuctionReadModel Auction { get; set; }
    }
}