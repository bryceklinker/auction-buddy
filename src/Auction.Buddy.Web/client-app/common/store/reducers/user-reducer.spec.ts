import { userReducer } from './user-reducer';
import { loginFailedAction, loginSuccessAction } from '../actions/user-actions';

describe('userReducer', () => {
    it('should have initial state', () => {
        const state = userReducer();

        expect(state.hasFailed).toEqual(false);
        expect(state.isSuccess).toEqual(false);
    });

    it('should have logged in user', () => {
        let state = userReducer();
        state = userReducer(
            state,
            loginSuccessAction({ isSuccess: true, tokenType: 'Be', expiresIn: 400, accessToken: 'something' }),
        );

        expect(state.isSuccess).toEqual(true);
        expect(state.tokenType).toEqual('Be');
        expect(state.accessToken).toEqual('something');
        expect(state.expiresIn).toEqual(400);
    });

    it('should have failed login true', () => {
        let state = userReducer();
        state = userReducer(state, loginFailedAction({ isSuccess: false }));

        expect(state.isSuccess).toEqual(false);
        expect(state.hasFailed).toEqual(true);
    });
});
