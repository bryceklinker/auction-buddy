import * as React from "react";
import {fireEvent} from '@testing-library/react';

import {renderWithStore} from "../../testing/render-with-store";
import {AuctionsListContainer} from "./AuctionsListContainer";
import {createMemoryHistory, MemoryHistory} from "history";

describe('AuctionsListContainer', () => {
    let history: MemoryHistory;
    
    beforeEach(() => {
        history = createMemoryHistory();    
    });
    
    it('should go to create auction', () => {
        const {getByTestId} = renderWithStore(<AuctionsListContainer history={history}/>);

        fireEvent.click(getByTestId('create-auction-button'));

        expect(history.entries).toContainEqual(expect.objectContaining({
            pathname: '/create-auction'
        }))
    })
});