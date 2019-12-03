const BASE_URL = 'https://localhost:5001';

const ALIASES = {
    CREATE_AUCTION: 'createAuction',
    GET_AUCTIONS: 'getAuctions',
    UPDATE_AUCTION: 'updateAuction'
};

function createAuction({name = 'IDK Auction', auctionDate = new Date()} = {}) {
    const body = {
        name,
        auctionDate: auctionDate.toISOString()
    };
    return cy.request('POST', `${BASE_URL}/auctions`, body)
        .as(ALIASES.CREATE_AUCTION);
}

function updateAuction({auctionId, name = null, auctionDate = null} = {}) {
    const body = {
        name, 
        auctionDate
    };
    
    return cy.request('PUT', `${BASE_URL}/auctions/${auctionId}`, body)
        .as(ALIASES.UPDATE_AUCTION);
}

function getAuctions() {
    return cy.request('GET', `${BASE_URL}/auctions`)
        .as(ALIASES.GET_AUCTIONS);
}

export const AuctionBuddyApi = {createAuction, getAuctions, updateAuction, ALIASES};