import { all } from 'redux-saga/effects';
import {loginSaga} from "./sagas/login-saga";
import {createAuctionSaga} from "./sagas/create-auction-saga";

export function* rootSaga() {
    yield all([
        loginSaga(),
        createAuctionSaga()
    ]);
}