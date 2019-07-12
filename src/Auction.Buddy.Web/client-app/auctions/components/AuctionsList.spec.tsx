import * as React from 'react';
import { cleanup, fireEvent } from '@testing-library/react';
import { push } from 'connected-react-router';

import { renderWithStore } from '../../testing/render-with-store';
import { AuctionsList } from './AuctionsList';
import { createTestingStore } from '../../testing/create-testing-store';
import { AuctionDto } from '../../common/store/dtos/auction-dto';
import { getAllAuctionsSuccessAction } from '../../common/store/actions/auction-actions';
import { createAuctionDto } from '../../testing/dto-factory';

describe('AuctionsListContainer', () => {
    it('should go to create auction', () => {
        const store = createTestingStore();
        const { getByTestId } = renderWithStore(<AuctionsList />, store);

        fireEvent.click(getByTestId('create-auction-button'));

        expect(store.getActions()).toContainEqual(push('/create-auction'));
    });

    it('should should show all auctions', () => {
        const dtos: AuctionDto[] = [createAuctionDto(), createAuctionDto(), createAuctionDto()];

        const store = createTestingStore(getAllAuctionsSuccessAction(dtos));
        const { getAllByTestId } = renderWithStore(<AuctionsList />, store);

        expect(getAllByTestId('auction-item')).toHaveLength(3);
    });

    it('should show auction information', () => {
        const dtos: AuctionDto[] = [createAuctionDto()];
        dtos[0].auctionDate = '2019-09-23';

        const store = createTestingStore(getAllAuctionsSuccessAction(dtos));
        const { getByTestId } = renderWithStore(<AuctionsList />, store);

        expect(getByTestId('auction-item')).toHaveTextContent(dtos[0].name);
        expect(getByTestId('auction-item')).toHaveTextContent('09/23/2019');
    });

    afterEach(() => cleanup());
});
