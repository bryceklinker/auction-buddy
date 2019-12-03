import {When, Then} from 'cypress-cucumber-preprocessor/steps';
import {AuctionBuddyApi} from '../../support/auction-buddy-api';

const FIVE_DAYS_IN_MILLISECONDS = 5 * 24 * 60 * 60 * 1000;
const AUCTION_DATE = new Date(new Date().getTime() + FIVE_DAYS_IN_MILLISECONDS);

When(/^I create a new auction$/, () => {
    AuctionBuddyApi.createAuction({name: 'Some Auction', auctionDate: AUCTION_DATE});
});

Then(/^I should see my newly created auction$/, () => {
    AuctionBuddyApi.getAuctions()
        .should(response => {
            const items = response.body.items;
            expect(items).to.have.length(1);
            expect(items[0]).to.have.property('name', 'Some Auction');
            expect(items[0]).to.have.property('auctionDate', AUCTION_DATE.toISOString());
        });
});