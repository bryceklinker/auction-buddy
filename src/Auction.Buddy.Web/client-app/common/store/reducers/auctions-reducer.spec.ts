import { auctionsReducer } from './auctions-reducer';
import {
    createAuctionFailedAction,
    createAuctionRequestAction,
    createAuctionSuccessAction,
    getAllAuctionsSuccessAction,
} from '../actions/auction-actions';
import { AuctionDto } from '../dtos/auction-dto';

describe('auctionsReducer', () => {
    it('should have initial auctions state', () => {
        const state = auctionsReducer();
        expect(state.auctions).toEqual([]);
        expect(state.validationResult).toBeNull();
    });

    it('should not be creating auction when create succeeds', () => {
        let state = auctionsReducer();
        state = auctionsReducer(state, createAuctionRequestAction({ name: 'bak', auctionDate: 'three' }));

        const newAuction = { id: 3, name: 'bob', auctionDate: 'jack' };
        state = auctionsReducer(state, createAuctionSuccessAction(newAuction));

        expect(state.auctions).toContainEqual(newAuction);
    });

    it('should not be creating auction when create fails', () => {
        let state = auctionsReducer();
        state = auctionsReducer(state, createAuctionRequestAction({ name: 'bak', auctionDate: 'three' }));

        state = auctionsReducer(state, createAuctionFailedAction({ isValid: false }));
        expect(state.validationResult).toEqual({ isValid: false });
    });

    it('should clear validation result when create requested', () => {
        let state = auctionsReducer();
        state = auctionsReducer(state, createAuctionRequestAction({ name: 'bak', auctionDate: 'three' }));
        state = auctionsReducer(state, createAuctionFailedAction({ isValid: false }));

        state = auctionsReducer(state, createAuctionRequestAction({ name: 'bak', auctionDate: 'three' }));

        expect(state.validationResult).toBeNull();
    });

    it('should set auctions in state', () => {
        const dtos: AuctionDto[] = [
            { id: 43, name: '', auctionDate: '' },
            { id: 5, name: '', auctionDate: '' },
            { id: 6, name: '', auctionDate: '' },
        ];

        let state = auctionsReducer();
        state = auctionsReducer(state, getAllAuctionsSuccessAction(dtos));

        expect(state.auctions).toContainEqual(dtos[0]);
        expect(state.auctions).toContainEqual(dtos[1]);
        expect(state.auctions).toContainEqual(dtos[2]);
    });
});
