import * as React from 'react';
import { fireEvent } from '@testing-library/react';
import { push } from 'connected-react-router';

import { renderWithStore } from '../../testing/render-with-store';
import { AuctionsList } from './AuctionsList';
import { createTestingStoreWithHistory } from '../../testing/create-testing-store';
import { createMemoryHistory } from 'history';
import { AuctionDto } from '../../common/store/dtos/auction-dto';
import { getAllAuctionsSuccessAction } from '../../common/store/actions/auction-actions';

describe('AuctionsListContainer', () => {
    it('should go to create auction', () => {
        const store = createTestingStoreWithHistory();
        const { getByTestId } = renderWithStore(<AuctionsList />, store);

        fireEvent.click(getByTestId('create-auction-button'));

        expect(store.getActions()).toContainEqual(push('/create-auction'));
    });

    it('should should show all auctions', () => {
        const dtos: AuctionDto[] = [
            { id: 6, name: '', auctionDate: '' },
            { id: 12, name: '', auctionDate: '' },
            { id: 7, name: '', auctionDate: '' },
        ];

        const store = createTestingStoreWithHistory(createMemoryHistory(), getAllAuctionsSuccessAction(dtos));
        const { getAllByTestId } = renderWithStore(<AuctionsList />, store);

        expect(getAllByTestId('auction-item')).toHaveLength(3);
    });
});
