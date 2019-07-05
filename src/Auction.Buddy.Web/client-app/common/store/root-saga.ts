import { all } from 'redux-saga/effects';
import {loginSaga} from "../../login/sagas/login-saga";

export function* rootSaga() {
    yield all([
        loginSaga()
    ]);
}