import { call, put, select, takeEvery } from 'redux-saga/effects';
import { LOCATION_CHANGE, LocationChangeAction } from 'connected-react-router';

import {
    AuctionActionTypes,
    getAllAuctionsFailedAction,
    getAllAuctionsRequestAction,
    getAllAuctionsSuccessAction,
} from '../actions/auction-actions';
import { authResultSelector } from '../reducers/user-reducer';
import { createAuthApi } from '../../services/create-auth-api';

function* getAllAuctions() {
    try {
        const authResult = yield select(authResultSelector);
        const authApi = yield createAuthApi(authResult);
        const dtos = yield call(authApi.get, '/api/auctions');
        yield put(getAllAuctionsSuccessAction(dtos));
    } catch (error) {
        yield put(getAllAuctionsFailedAction(error));
    }
}

function* locationChanges(action: LocationChangeAction) {
    if (action.payload.location.pathname === '/auctions') {
        yield put(getAllAuctionsRequestAction());
    }
}

export function* getAllAuctionsSaga() {
    yield takeEvery(AuctionActionTypes.GET_ALL_REQUEST, getAllAuctions);
    yield takeEvery(LOCATION_CHANGE, locationChanges);
}
