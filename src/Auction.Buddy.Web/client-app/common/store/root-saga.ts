import { all } from 'redux-saga/effects';
import {loginSaga} from "./sagas/login-saga";

export function* rootSaga() {
    yield all([
        loginSaga()
    ]);
}