import { takeEvery, put, call } from 'redux-saga/effects';
import { push } from 'connected-react-router';

import { api } from '../../services/api';
import {
    loginFailedAction,
    loginSuccessAction,
    UserActionTypes,
    UserLoginRequestAction,
} from '../actions/user-actions';
import { AuthenticationResultDto } from '../dtos/authentication-result-dto';
import { saveAuth } from '../auth-storage';

export function* performLogin(action: UserLoginRequestAction) {
    const result: AuthenticationResultDto = yield call(api.post, '/authentication/login', action.payload);
    saveAuth(result);
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
