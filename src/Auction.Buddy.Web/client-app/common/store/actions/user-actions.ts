import { action, PayloadAction } from 'typesafe-actions';
import { CredentialsDto } from '../dtos/credentials-dto';
import { AuthenticationResultDto } from '../dtos/authentication-result-dto';
import { InitAction } from './init-actions';

export const UserActionTypes = {
    LOGIN_REQUEST: '[User] LoginView Request',
    LOGIN_SUCCESS: '[User] LoginView Success',
    LOGIN_FAILED: '[User] LoginView Failed',
};

export interface UserLoginRequestAction extends PayloadAction<string, CredentialsDto> {
    type: typeof UserActionTypes.LOGIN_REQUEST;
}

export function loginRequestAction(dto: CredentialsDto): UserLoginRequestAction {
    return action(UserActionTypes.LOGIN_REQUEST, dto);
}

export interface UserLoginSuccessAction extends PayloadAction<string, AuthenticationResultDto> {
    type: typeof UserActionTypes.LOGIN_SUCCESS;
}

export function loginSuccessAction(dto: AuthenticationResultDto): UserLoginSuccessAction {
    return action(UserActionTypes.LOGIN_SUCCESS, dto);
}

export interface UserLoginFailedAction extends PayloadAction<string, AuthenticationResultDto> {
    type: typeof UserActionTypes.LOGIN_FAILED;
}

export function loginFailedAction(dto: AuthenticationResultDto): UserLoginFailedAction {
    return action(UserActionTypes.LOGIN_FAILED, dto);
}

export type UserActions = UserLoginRequestAction | UserLoginSuccessAction | UserLoginFailedAction | InitAction;
