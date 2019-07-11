import { loadingReducer } from './loading-reducer';
import {
    createAuctionRequestAction,
    getAllAuctionsFailedAction,
    getAllAuctionsRequestAction,
    getAllAuctionsSuccessAction,
} from '../actions/auction-actions';
import { loginRequestAction } from '../actions/user-actions';

describe('loadingReducer', () => {
    it('should not be loading anything', () => {
        const state = loadingReducer();
        expect(state).toEqual({});
    });

    it('should be loading auctions', () => {
        let state = loadingReducer();
        state = loadingReducer(state, getAllAuctionsRequestAction());

        expect(state['[Auctions] Get All Auctions']).toEqual(true);
    });

    it('should be creating auction', () => {
        let state = loadingReducer();
        state = loadingReducer(state, createAuctionRequestAction({ name: '', auctionDate: '' }));

        expect(state['[Auctions] Create Auction']).toEqual(true);
    });

    it('should be logging in', () => {
        let state = loadingReducer();
        state = loadingReducer(state, loginRequestAction({ username: '', password: '' }));

        expect(state['[User] LoginView']).toEqual(true);
    });

    it('should not be loading auctions when success', () => {
        let state = loadingReducer();
        state = loadingReducer(state, getAllAuctionsRequestAction());
        state = loadingReducer(state, getAllAuctionsSuccessAction([]));

        expect(state['[Auctions] Get All Auctions']).toEqual(false);
    });

    it('should not be loading auctions when failed', () => {
        let state = loadingReducer();
        state = loadingReducer(state, getAllAuctionsRequestAction());
        state = loadingReducer(state, getAllAuctionsFailedAction('bad'));

        expect(state['[Auctions] Get All Auctions']).toEqual(false);
    });
});
