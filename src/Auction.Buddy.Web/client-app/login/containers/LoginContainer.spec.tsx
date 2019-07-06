import * as React from "react";
import {cleanup, fireEvent} from '@testing-library/react';
import {createMemoryHistory, MemoryHistory} from "history";

import {LoginContainer} from "./LoginContainer";
import {loginFailedAction, loginRequestAction} from "../../common/store/actions/user-actions";
import {createTestingStore} from "../../testing/testing-store";
import {renderWithStore} from "../../testing/render-with-store";

describe('LoginContainer', () => {
    let history: MemoryHistory;

    beforeEach(() => {
        history = createMemoryHistory();
        fetchMock.mockResponse(JSON.stringify({isSuccess: true}));
    });

    it('should log user in', () => {
        const store = createTestingStore();
        const {getByTestId} = renderWithStore(<LoginContainer/>, store);
        login(getByTestId, 'bob', 'jack');

        expect(store.getActions()).toContainEqual(loginRequestAction({username: 'bob', password: 'jack'}));
    });
    
    it('should disable login button when logging in', () => {
        const store = createTestingStore(history, loginRequestAction({username: 'bob', password: 'three' }));
        const {getByTestId} = renderWithStore(<LoginContainer />, store);
        
        expect(getByTestId('login-button')).toBeDisabled();
    })

    it('should show error when login has failed', () => {
        const store = createTestingStore(history, loginFailedAction({isSuccess: false}));
        const {getByTestId} = renderWithStore(<LoginContainer />, store);
        
        expect(getByTestId('login-error').textContent).toContain('Invalid username or password');
    });

    afterEach(() => cleanup());

    function login(getByTestId: (testId: string) => Element, username: string, password: string) {
        fireEvent.change(getByTestId('username-input'), {target: {value: username}});
        fireEvent.change(getByTestId('password-input'), {target: {value: password}});
        fireEvent.click(getByTestId('login-button'));
    }
});