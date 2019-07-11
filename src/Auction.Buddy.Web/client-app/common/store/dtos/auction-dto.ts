export interface CreateAuctionDto {
    name: string;
    auctionDate: string;
}

export interface AuctionDto extends CreateAuctionDto {
    id: number;
}
