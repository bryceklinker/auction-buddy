import {takeEvery, put, call} from 'redux-saga/effects';
import {PayloadAction} from "typesafe-actions";
import {push} from "connected-react-router";

import {CredentialsDto} from "../dtos/credentials-dto";
import {api} from "../../services/api";
import {loginFailedAction, loginSuccessAction, UserActionTypes} from "../actions/user-actions";
import {AuthenticationResultDto} from "../dtos/authentication-result-dto";

export function* performLogin(action: PayloadAction<string, CredentialsDto>) {
    const result: AuthenticationResultDto = yield call(api.post, '/authentication/login', action.payload);
    if (result.isSuccess) {
        yield put(loginSuccessAction(result));
        yield put(push('/auctions'));
    } else {
        yield put(loginFailedAction(result));
    }
}

export function* loginSaga() {
    yield takeEvery(UserActionTypes.LOGIN_REQUEST, performLogin);
}