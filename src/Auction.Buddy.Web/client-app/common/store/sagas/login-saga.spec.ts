import SagaTester from 'redux-saga-tester';

import {loginSaga} from "./login-saga";
import {loginFailedAction, loginRequestAction, loginSuccessAction, UserActionTypes} from "../actions/user-actions";
import {AppState} from "../../../app-state";

describe('loginSaga', () => {
    let tester: SagaTester<AppState>;
        
    beforeEach(() => {
        tester = new SagaTester<AppState>();
        tester.start(loginSaga);
    });
    
    it('should post credentials to api', async () => {
        fetchMock.mockResponse(JSON.stringify({ isSuccess: true }));
        
        tester.dispatch(loginRequestAction({ username: 'bob', password: 'pwd' }));
        await tester.waitFor(UserActionTypes.LOGIN_SUCCESS);
        
        expect(fetch).toHaveBeenCalledWith('/authentication/login', expect.objectContaining({
            method: 'POST',
            body: JSON.stringify({ username: 'bob', password: 'pwd' })
        }));
    });
    
    it('should dispatch login success action', async () => {
        fetchMock.mockResponse(JSON.stringify({ isSuccess: true }));
        
        tester.dispatch(loginRequestAction({ username: 'bob', password: 'pwd' }));
        await tester.waitFor(UserActionTypes.LOGIN_SUCCESS);
        
        expect(tester.getCalledActions()).toContainEqual(loginSuccessAction({ isSuccess: true }))
    });
    
    it('should dispatch login failed action', async () => {
        fetchMock.mockResponse(JSON.stringify({ isSuccess: false }));

        tester.dispatch(loginRequestAction({ username: 'bob', password: 'pwd' }));
        await tester.waitFor(UserActionTypes.LOGIN_FAILED);

        expect(tester.getCalledActions()).toContainEqual(loginFailedAction({ isSuccess: false }))
    })
});