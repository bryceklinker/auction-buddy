import {Given, When, Then, Before} from 'cypress-cucumber-preprocessor/steps';
import {AuctionBuddyApi} from '../../support/auction-buddy-api';

Before(() => {
    AuctionBuddyApi.clearAll();
});

Given(/^an existing auction$/, () => {
   AuctionBuddyApi.createAuction(); 
});

When(/^I update the auction$/, () => {
    AuctionBuddyApi.getAuctions().then(response => {
        const auction = response.body.items[0];
        AuctionBuddyApi.updateAuction({auctionId: auction.id, name: 'bobo'});
    });
});

Then(/^the auction has been updated$/, () => {
    AuctionBuddyApi.getAuctions().should(response => {
        const auction = response.body.items[0];
        expect(auction).to.have.property('name', 'bobo');
    })
});