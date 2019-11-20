const BASE_URL = 'https://localhost:5001';

const ALIASES = {
    CREATE_AUCTION: 'createAuction',
    GET_AUCTIONS: 'getAuctions'
};

function createAuction({name = 'IDK Auction', auctionDate = new Date()} = {}) {
    const body = {
        name,
        auctionDate: auctionDate.toISOString()
    };
    return cy.request('POST', `${BASE_URL}/auctions`, body)
        .as(ALIASES.CREATE_AUCTION);
}

function getAuctions() {
    return cy.request('GET', `${BASE_URL}/auctions`)
        .as(ALIASES.GET_AUCTIONS);
}

export const AuctionBuddyApi = {createAuction, getAuctions, ALIASES};