import {Before, Given, Then, When} from 'cypress-cucumber-preprocessor/steps';
import {AuctionBuddyApi} from '../../support/auction-buddy-api';

const ITEM_NAME = 'Some Name';
let auctionId;

Before(() => {
    AuctionBuddyApi.clearAll();
});

Given(/^an auction with one item$/, () => {
    AuctionBuddyApi.createAuction().then(res => {
        auctionId = res.headers['location'].split('/').pop();
        AuctionBuddyApi.addAuctionItem({auctionId, name: ITEM_NAME, donor: 'jacky' });
    });
});

When(/^I update the auction item$/, () => {
    AuctionBuddyApi.updateAuctionItem({
        auctionId, 
        itemName: ITEM_NAME, 
        newName: 'New Hotness',
        newQuantity: 5,
        newDescription: 'Old n Busted',
        newDonor: 'Susan'
    })
});

Then(/^I should see the updated auction item$/, () => {
    AuctionBuddyApi.getAuctionItems({auctionId}).should(res => {
        const item = res.body.items.find(i => i.name === 'New Hotness');
        expect(item).to.have.property('quantity', 5);
        expect(item).to.have.property('description', 'Old n Busted');
        expect(item).to.have.property('donor', 'Susan');
    });
});