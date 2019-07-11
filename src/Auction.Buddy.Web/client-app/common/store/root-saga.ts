import { all } from 'redux-saga/effects';
import { loginSaga } from './sagas/login-saga';
import { createAuctionSaga } from './sagas/create-auction-saga';
import { getAllAuctionsSaga } from './sagas/get-auctions-saga';

export function* rootSaga() {
    yield all([loginSaga(), createAuctionSaga(), getAllAuctionsSaga()]);
}
