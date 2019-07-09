import {createSelector} from 'reselect';

import {AppState} from "../../../app-state";
import {initAction} from "../actions/init-actions";
import {AuthenticationResultDto} from "../dtos/authentication-result-dto";
import {UserActions, UserActionTypes, UserLoginSuccessAction} from "../actions/user-actions";

export interface UserState extends AuthenticationResultDto {
    hasFailed: boolean;
    isLoggingIn: boolean;
}

const initialState: UserState = {
    isSuccess: false,
    hasFailed: false,
    isLoggingIn: false
};

export function userReducer(state: UserState = initialState, action: UserActions = initAction()): UserState {
    switch (action.type) {
        case UserActionTypes.LOGIN_REQUEST:
            return {...state, isLoggingIn: true};

        case UserActionTypes.LOGIN_SUCCESS:
            return {...state, ...(action as UserLoginSuccessAction).payload, isLoggingIn: false};

        case UserActionTypes.LOGIN_FAILED:
            return {...state, hasFailed: true, isLoggingIn: false};

        default:
            return state;
    }
}

function selectUserState(state: AppState): UserState {
    return state.user;
}

export const hasLoginFailedSelector = createSelector(selectUserState, s => s.hasFailed);
export const isLoggingInSelector = createSelector(selectUserState, s => s.isLoggingIn);
export const authResultSelector = createSelector(selectUserState, s => s);