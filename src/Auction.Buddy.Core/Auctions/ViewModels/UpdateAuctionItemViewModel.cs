namespace Auction.Buddy.Core.Auctions.ViewModels
{
    public class UpdateAuctionItemViewModel
    {
        public string NewName { get; set; }
        public string NewDonor { get; set; }
        public string NewDescription { get; set; }
        public int? NewQuantity { get; set; }
    }
}