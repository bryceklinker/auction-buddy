import {Before, Given, When, Then} from 'cypress-cucumber-preprocessor/steps';
import {AuctionBuddyApi} from '../../support/auction-buddy-api';

Before(() => {
   AuctionBuddyApi.clearAll(); 
});

Given(/^an existing auction$/, () => {
    AuctionBuddyApi.createAuction();
});

When(/^I add an item to the auction$/, () => {
    AuctionBuddyApi.getAuctions().then(response => {
        const auction = response.body.items[0];
        AuctionBuddyApi.addAuctionItem({auctionId: auction.id, name: 'new-hotness', donor: 'john smith'});
    });
});

Then(/^I should see one item in the auction$/, () => {
   AuctionBuddyApi.getAuctions().should(auctionsResponse => {
       const auction = auctionsResponse.body.items[0];
       expect(auction).to.have.property('itemCount', 1);
       
       AuctionBuddyApi.getAuctionItems({auctionId: auction.id}).should(itemsResponse => {
           const items = itemsResponse.body.items;
           expect(items).to.have.length(1);

           const item = items[0];
           expect(item).to.have.property('name', 'new-hotness');
           expect(item).to.have.property('donor', 'john smith');
           expect(item).to.have.property('quantity', 1);
           expect(item).to.have.property('description', null);
       })
   });
});