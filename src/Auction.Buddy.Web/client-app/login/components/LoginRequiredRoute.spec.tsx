import React from "react";
import {createTestingStore} from "../../testing/create-testing-store";
import {renderWithStore} from "../../testing/render-with-store";
import {AppState} from "../../app-state";
import {Store} from "redux";
import {MemoryRouter, RouteComponentProps} from "react-router";
import { LoginRequiredRoute } from './LoginRequiredRoute';
import {push} from "connected-react-router";
import {loginSuccessAction} from "../../common/store/actions/user-actions";
import {createSuccessAuthenticationResultDto} from "../../testing/dto-factory";

describe('LoginRequiredRoute', () => {
    it('should navigate to login', () => {
        const store = createTestingStore();
        const { queryAllByTestId } = renderWithRouter(FakeComponent, store);

        expect(queryAllByTestId('bob')).toHaveLength(0);
        expect(store.getActions()).toContainEqual(push('/login'));
    });

    it('should show component in route', () => {
        const store = createTestingStore(loginSuccessAction(createSuccessAuthenticationResultDto()));
        const { queryAllByTestId } = renderWithRouter(FakeComponent, store);

        expect(queryAllByTestId('bob')).toHaveLength(1);
    });

    function renderWithRouter(
        component: React.ComponentType<RouteComponentProps<any>> | React.ComponentType<any>,
        store: Store<AppState>,
    ) {
        return renderWithStore(
            <MemoryRouter>
                <LoginRequiredRoute component={component} />
            </MemoryRouter>,
            store,
        );
    }
    
    function FakeComponent() {
        return <div data-testid="bob" />
    }
})