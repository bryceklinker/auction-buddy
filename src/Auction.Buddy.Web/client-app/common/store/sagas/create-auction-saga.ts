import {PayloadAction} from "typesafe-actions";

import {CreateAuctionDto} from "../dtos/auction-dto";
import {takeEvery, put, call} from "redux-saga/effects";
import {AuctionActionTypes, createAuctionFailedAction, createAuctionSuccessAction} from "../actions/auction-actions";
import {api} from "../../services/api";
import {push} from "connected-react-router";

function* performCreateAuction(action: PayloadAction<string, CreateAuctionDto>) {
    try {
        const data = yield call(api.post, '/api/auctions', action.payload);
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