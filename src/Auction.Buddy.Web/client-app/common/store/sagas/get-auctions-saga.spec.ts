import SagaTester from 'redux-saga-tester';
import { createMemoryHistory } from 'history';

import { AppState } from '../../../app-state';
import { createState } from '../../../testing/create-testing-store';
import { loginSuccessAction } from '../actions/user-actions';
import { AuthenticationResultDto } from '../dtos/authentication-result-dto';
import {
    AuctionActionTypes,
    getAllAuctionsFailedAction,
    getAllAuctionsRequestAction,
    getAllAuctionsSuccessAction,
} from '../actions/auction-actions';
import { AuctionDto } from '../dtos/auction-dto';
import { getAllAuctionsSaga } from './get-auctions-saga';
import { LOCATION_CHANGE } from 'connected-react-router';

describe('getAuctionsSaga', () => {
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
        tester.start(getAllAuctionsSaga);
    });

    it('should dispatch get all auctions success action', async () => {
        const dtos: AuctionDto[] = [
            { id: 5, name: '', auctionDate: '' },
            { id: 3, name: '', auctionDate: '' },
            { id: 8, name: '', auctionDate: '' },
        ];
        fetchMock.mockResponse(JSON.stringify(dtos));

        tester.dispatch(getAllAuctionsRequestAction());
        await tester.waitFor(AuctionActionTypes.GET_ALL_SUCCESS);

        expect(tester.getCalledActions()).toContainEqual(getAllAuctionsSuccessAction(dtos));
    });

    it('should get data from api', async () => {
        const dtos: AuctionDto[] = [
            { id: 5, name: '', auctionDate: '' },
            { id: 3, name: '', auctionDate: '' },
            { id: 8, name: '', auctionDate: '' },
        ];
        fetchMock.mockResponse(JSON.stringify(dtos));

        tester.dispatch(getAllAuctionsRequestAction());
        await tester.waitFor(AuctionActionTypes.GET_ALL_SUCCESS);

        expect(fetch).toHaveBeenCalledWith(
            '/api/auctions',
            expect.objectContaining({
                headers: expect.objectContaining({
                    Authorization: 'Bearer header.payload.signature',
                }),
            }),
        );
    });

    it('should dispatch get all auctions failed action', async () => {
        fetchMock.mockResponse(JSON.stringify({}), { status: 500 });

        tester.dispatch(getAllAuctionsRequestAction());
        await tester.waitFor(AuctionActionTypes.GET_ALL_FAILED);

        expect(tester.getCalledActions()).toContainEqual(getAllAuctionsFailedAction(expect.anything()));
    });

    it('should dispatch get all auctions request action when navigate to auctions', async () => {
        tester.dispatch({ type: LOCATION_CHANGE, payload: { location: { pathname: '/auctions' } } });

        await tester.waitFor(AuctionActionTypes.GET_ALL_REQUEST);
        expect(tester.getCalledActions()).toContainEqual(getAllAuctionsRequestAction());
    });
});
