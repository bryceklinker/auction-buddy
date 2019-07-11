import { createSelector } from 'reselect';

import { AppState } from '../../../app-state';
import { initAction } from '../actions/init-actions';
import { AuthenticationResultDto } from '../dtos/authentication-result-dto';
import { UserActions, UserActionTypes, UserLoginSuccessAction } from '../actions/user-actions';

export interface UserState extends AuthenticationResultDto {
    hasFailed: boolean;
}

const initialState: UserState = {
    isSuccess: false,
    hasFailed: false,
};

export function userReducer(state: UserState = initialState, action: UserActions = initAction()): UserState {
    switch (action.type) {
        case UserActionTypes.LOGIN_SUCCESS:
            return { ...state, ...(action as UserLoginSuccessAction).payload };

        case UserActionTypes.LOGIN_FAILED:
            return { ...state, hasFailed: true };

        default:
            return state;
    }
}

function selectUserState(state: AppState): UserState {
    return state.user;
}

export const hasLoginFailedSelector = createSelector(
    selectUserState,
    s => s.hasFailed,
);
export const authResultSelector = createSelector(
    selectUserState,
    s => s,
);
export const isUserLoggedInSelector = createSelector(
    selectUserState,
    s => s.isSuccess,
);
