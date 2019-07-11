import SagaTester from 'redux-saga-tester';
import { AppState } from '../../../app-state';
import { createAuctionSaga } from './create-auction-saga';
import {
    AuctionActionTypes,
    createAuctionFailedAction,
    createAuctionRequestAction,
    createAuctionSuccessAction,
} from '../actions/auction-actions';
import { AuctionDto } from '../dtos/auction-dto';
import { CALL_HISTORY_METHOD, push } from 'connected-react-router';
import { createState } from '../../../testing/create-testing-store';
import { createMemoryHistory } from 'history';
import { AuthenticationResultDto } from '../dtos/authentication-result-dto';
import { loginSuccessAction } from '../actions/user-actions';

describe('createAuctionSaga', () => {
    let tester: SagaTester<AppState>;
    let authResult: AuthenticationResultDto;

    beforeEach(() => {
        authResult = {
            isSuccess: true,
            tokenType: 'Bearer',
            accessToken: 'header.payload.signature',
        };
        tester = new SagaTester<AppState>({
            initialState: createState(createMemoryHistory(), loginSuccessAction(authResult)),
        });
        tester.start(createAuctionSaga);
    });

    it('should dispatch create auction success action', async () => {
        fetchMock.mockResponse(JSON.stringify({ id: 5, name: 'three', auctionDate: '2019-03-04' }));

        tester.dispatch(createAuctionRequestAction({ name: 'three', auctionDate: '03/04/2019' }));
        await tester.waitFor(AuctionActionTypes.CREATE_SUCCESS);

        const expectedAuction: AuctionDto = { id: 5, name: 'three', auctionDate: '2019-03-04' };
        expect(tester.getCalledActions()).toContainEqual(createAuctionSuccessAction(expectedAuction));
    });

    it('should navigate to auction detail', async () => {
        fetchMock.mockResponse(JSON.stringify({ id: 5, name: 'three', auctionDate: '2019-03-04' }));

        tester.dispatch(createAuctionRequestAction({ name: 'three', auctionDate: '03/04/2019' }));
        await tester.waitFor(CALL_HISTORY_METHOD);

        expect(tester.getCalledActions()).toContainEqual(push('/auction-detail/5'));
    });

    it('should post data to api', async () => {
        fetchMock.mockResponse(JSON.stringify({ id: 5, name: 'three', auctionDate: '2019-03-04' }));

        tester.dispatch(createAuctionRequestAction({ name: 'three', auctionDate: '03/04/2019' }));
        await tester.waitFor(AuctionActionTypes.CREATE_SUCCESS);

        expect(fetch).toHaveBeenCalledWith(
            '/api/auctions',
            expect.objectContaining({
                method: 'POST',
                body: JSON.stringify({ name: 'three', auctionDate: '03/04/2019' }),
                headers: expect.objectContaining({
                    Authorization: 'Bearer header.payload.signature',
                }),
            }),
        );
    });

    it('should dispatch create auction failed action', async () => {
        fetchMock.mockResponse(JSON.stringify({ isValid: false }), { status: 400 });

        tester.dispatch(createAuctionRequestAction({ name: 'three', auctionDate: '03/04/2019' }));
        await tester.waitFor(AuctionActionTypes.CREATE_FAILED);

        expect(tester.getCalledActions()).toContainEqual(createAuctionFailedAction({ isValid: false }));
    });
});
