import { select } from 'redux-saga/effects';
import {push} from "connected-react-router";
import {takeEvery, put, call} from "redux-saga/effects";

import {
    AuctionActionTypes,
    createAuctionFailedAction,
    CreateAuctionRequestAction,
    createAuctionSuccessAction
} from "../actions/auction-actions";
import {createAuthApi} from "../../services/create-auth-api";
import {authResultSelector} from "../reducers/user-reducer";

function* performCreateAuction(action: CreateAuctionRequestAction) {
    try {
        const authResult = yield select(authResultSelector);
        const authApi = yield createAuthApi(authResult);
        const data = yield call(authApi.post, '/api/auctions', action.payload);
        yield put(createAuctionSuccessAction(data));
        yield put(push(`/auction-detail/${data.id}`));
    } catch (err) {
        const data = JSON.parse(err.message);
        yield put(createAuctionFailedAction(data));
    }
}

export function* createAuctionSaga() {
    yield takeEvery(AuctionActionTypes.CREATE_REQUEST, performCreateAuction);
}