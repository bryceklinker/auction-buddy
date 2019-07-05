import * as React from "react";
import {fireEvent} from '@testing-library/react';

import {createTestingStore} from "../../testing/testing-store";
import {renderWithStore} from "../../testing/render-with-store";
import {createAuctionRequestAction} from "../../common/store/actions/auction-actions";
import {CreateAuctionDto} from "../../common/store/dtos/auction-dto";
import {CreateAuctionContainer} from "./CreateAuctionContainer";

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
});