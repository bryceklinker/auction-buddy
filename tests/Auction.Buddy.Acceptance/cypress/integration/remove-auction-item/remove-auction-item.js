import {Before, Given, Then, When} from 'cypress-cucumber-preprocessor/steps';
import {AuctionBuddyApi} from '../../support/auction-buddy-api';

const ITEM_NAME = 'Shoes';

let auctionId;
Before(() => {
    AuctionBuddyApi.clearAll();
});

Given(/^an auction with one item$/, () => {
    AuctionBuddyApi.createAuction().then(res => {
        auctionId = res.headers['location'].split('/').pop();
        AuctionBuddyApi.addAuctionItem({auctionId, name: ITEM_NAME, donor: 'billy' });
    });
});

When(/^I remove an item from the auction$/, () => {
    AuctionBuddyApi.removeAuctionItem({auctionId, itemName: ITEM_NAME});
});

Then(/^I should see no items in the auction$/, () => {
    AuctionBuddyApi.getAuctionItems({auctionId}).should(res => {
        expect(res.body.items).to.have.length(0);
    });
});