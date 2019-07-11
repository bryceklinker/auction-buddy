import * as React from 'react';
import { createMemoryHistory, History } from 'history';
import { Store } from 'redux';
import { cleanup } from '@testing-library/react';
import { ConnectedRouter } from 'connected-react-router';
import { Route } from 'react-router';

import { createTestingStoreWithHistory } from '../../testing/create-testing-store';
import { createAuctionSuccessAction } from '../../common/store/actions/auction-actions';
import { AuctionDto } from '../../common/store/dtos/auction-dto';
import { renderWithStore } from '../../testing/render-with-store';
import { AuctionDetail } from './AuctionDetail';
import { AppState } from '../../app-state';

describe('AuctionDetail', () => {
    it('should show auction info', () => {
        const dto: AuctionDto = {
            id: 6,
            name: 'john',
            auctionDate: '2019-04-21',
        };
        const history = createMemoryHistory({ initialEntries: ['/auction-detail/6'] });
        const store = createTestingStoreWithHistory(history, createAuctionSuccessAction(dto));
        const { getByTestId } = renderWithStoreAndRouter(history, store);

        expect(getByTestId('auction-name')).toHaveTextContent('john');
        expect(getByTestId('auction-date')).toHaveTextContent('04/21/2019');
    });

    afterEach(() => cleanup());

    function renderWithStoreAndRouter(history: History, store: Store<AppState>) {
        return renderWithStore(
            <ConnectedRouter history={history}>
                <Route path={'/auction-detail/:id'} component={AuctionDetail} />
            </ConnectedRouter>,
            store,
        );
    }
});
