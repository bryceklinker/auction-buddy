import * as React from "react";
import {createMemoryHistory, History} from "history";
import {Store} from "redux";
import {cleanup} from "@testing-library/react";
import {ConnectedRouter} from "connected-react-router";
import {Route} from "react-router";

import {createTestingStore} from "../../testing/testing-store";
import {createAuctionSuccessAction} from "../../common/store/actions/auction-actions";
import {AuctionDto} from "../../common/store/dtos/auction-dto";
import {renderWithStore} from "../../testing/render-with-store";
import {AuctionDetailContainer} from "./AuctionDetailContainer";
import {AppState} from "../../app-state";

describe('AuctionDetailContainer', () => {
    it('should show auction info', () => {
        const dto: AuctionDto = {
            id: 6,
            name: 'john',
            auctionDate: '2019-04-21'
        };
        const history = createMemoryHistory({initialEntries: ['/auction-detail/6']});
        const store = createTestingStore(history, createAuctionSuccessAction(dto));
        const {getByTestId} = renderWithStoreAndRouter(history, store);

        expect(getByTestId('auction-name')).toHaveTextContent('john');
        expect(getByTestId('auction-date')).toHaveTextContent('04/21/2019');
    });

    afterEach(() => cleanup());

    function renderWithStoreAndRouter(history: History, store: Store<AppState>) {
        return renderWithStore(
            <ConnectedRouter history={history}>
                <Route path={'/auction-detail/:id'} component={AuctionDetailContainer}/>
            </ConnectedRouter>,
            store
        );
    }
});