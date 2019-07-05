import {auctionsReducer} from "./auctions-reducer";
import {
    createAuctionFailedAction,
    createAuctionRequestAction,
    createAuctionSuccessAction
} from "../actions/auction-actions";

describe('auctionsReducer', () => {
    it('should have initial auctions state', () => {
        const state = auctionsReducer();
        expect(state.auctions).toEqual([]);
        expect(state.isCreating).toEqual(false);
    });

    it('should be creating auction', () => {
        let state = auctionsReducer();
        state = auctionsReducer(state, createAuctionRequestAction({name: 'bob', auctionDate: 'three'}));
        expect(state.isCreating).toEqual(true);
    });

    it('should not be creating auction when create succeeds', () => {
        let state = auctionsReducer();
        state = auctionsReducer(state, createAuctionRequestAction({name: 'bak', auctionDate: 'three'}));

        const newAuction = {id: 3, name: 'bob', auctionDate: 'jack'};
        state = auctionsReducer(state, createAuctionSuccessAction(newAuction));

        expect(state.auctions).toContainEqual(newAuction);
        expect(state.isCreating).toEqual(false);
    });

    it('should not be creating auction when create fails', () => {
        let state = auctionsReducer();
        state = auctionsReducer(state, createAuctionRequestAction({name: 'bak', auctionDate: 'three'}));

        state = auctionsReducer(state, createAuctionFailedAction({isValid: false}));
        expect(state.createError).toEqual({isValid: false});
        expect(state.isCreating).toEqual(false);
    })
});