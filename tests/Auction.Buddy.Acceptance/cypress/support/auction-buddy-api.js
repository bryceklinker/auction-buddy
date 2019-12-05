const BASE_URL = 'https://localhost:5001';

const ALIASES = {
    CREATE_AUCTION: 'createAuction',
    GET_AUCTIONS: 'getAuctions',
    UPDATE_AUCTION: 'updateAuction',
    ADD_AUCTION_ITEM: 'addAuctionItem',
    GET_AUCTION_ITEMS: 'getAuctionItems',
    CLEAR_ALL: 'clearAll',
    REMOVE_AUCTION_ITEM: 'removeAuctionItem'
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

function addAuctionItem({auctionId, name, donor, description = null, quantity = 1} = {}) {
    const body = {
        name, donor, description, quantity
    };
    
    return cy.request('POST', `${BASE_URL}/auctions/${auctionId}/items`, body)
        .as(ALIASES.ADD_AUCTION_ITEM);
}

function removeAuctionItem({auctionId, itemName} = {}) {
    const encodedName = encodeURIComponent(itemName);
    return cy.request('DELETE', `${BASE_URL}/auctions/${auctionId}/items/${encodedName}`)
        .as(ALIASES.REMOVE_AUCTION_ITEM);
}

function getAuctionItems({auctionId} = {}) {
    return cy.request('GET', `${BASE_URL}/auctions/${auctionId}/items`)
        .as(ALIASES.GET_AUCTION_ITEMS)
}

function clearAll() {
    return cy.request('DELETE', `${BASE_URL}/testing/clear-all`)
        .as(ALIASES.CLEAR_ALL)
}

export const AuctionBuddyApi = {
    clearAll, 
    createAuction, 
    getAuctions, 
    updateAuction, 
    addAuctionItem,
    removeAuctionItem,
    getAuctionItems, 
    ALIASES
};