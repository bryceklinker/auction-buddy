import * as React from "react";
import {cleanup, fireEvent} from '@testing-library/react';

import {createTestingStore} from "../../testing/testing-store";
import {renderWithStore} from "../../testing/render-with-store";
import {createAuctionRequestAction} from "../../common/store/actions/auction-actions";
import {CreateAuctionDto} from "../../common/store/dtos/auction-dto";
import {CreateAuctionContainer} from "./CreateAuctionContainer";
import {createMemoryHistory} from "history";

describe('CreateAuctionContainer', () => {
    it('should dispatch create auction request', () => {
        const store = createTestingStore();

        const {getByTestId} = renderWithStore(<CreateAuctionContainer />, store);
        fireEvent.change(getByTestId('create-auction-name-input'), {target: {value: 'Harvest Home'}});
        fireEvent.change(getByTestId('create-auction-date-input'), {target: {value: '03/12/2019'}});
        fireEvent.click(getByTestId('create-auction-save-button'));

        const dto: CreateAuctionDto = { name: 'Harvest Home', auctionDate: '03/12/2019' };
        expect(store.getActions()).toContainEqual(createAuctionRequestAction(dto));
    });
    
    it('should disable create auction button when creating auction', () => {
        const store = createTestingStore(createMemoryHistory(), createAuctionRequestAction({ name: 'one', auctionDate: 'bob'} ));
        const {getByTestId} = renderWithStore(<CreateAuctionContainer/>, store);
        
        expect(getByTestId('create-auction-save-button')).toBeDisabled()
    });
    
    afterEach(() => cleanup())
});