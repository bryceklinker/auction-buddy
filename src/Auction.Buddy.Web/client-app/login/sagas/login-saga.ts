import { takeEvery, put} from 'redux-saga/effects';
import {LoginActionTypes, loginSuccessAction} from "../actions";
import {PayloadAction} from "typesafe-actions";
import {CredentialsDto} from "../dtos/credentials-dto";
import {AuthenticationResultDto} from "../dtos/authentication-result-dto";

export function* performLogin(_: PayloadAction<string, CredentialsDto>) {
    const response = yield fetch('/authentication/login');
    const result: AuthenticationResultDto = yield response.json();
    yield put(loginSuccessAction(result));
}

export function* loginSaga() {
    yield takeEvery(LoginActionTypes.LOGIN_REQUEST, performLogin);
}