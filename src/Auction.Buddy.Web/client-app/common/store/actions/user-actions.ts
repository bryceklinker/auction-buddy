import {action, PayloadAction} from 'typesafe-actions';
import {CredentialsDto} from "../dtos/credentials-dto";
import {AuthenticationResultDto} from "../dtos/authentication-result-dto";

export const UserActionTypes = {
    LOGIN_REQUEST: '[User] Login Request',
    LOGIN_SUCCESS: '[User] Login Success',
    LOGIN_FAILED: '[User] Login Failed'
};

export function loginRequestAction(dto: CredentialsDto): PayloadAction<string, CredentialsDto> {
    return action(UserActionTypes.LOGIN_REQUEST, dto);
}

export function loginSuccessAction(dto: AuthenticationResultDto): PayloadAction<string, AuthenticationResultDto> {
    return action(UserActionTypes.LOGIN_SUCCESS, dto);
}

export function loginFailedAction(dto: AuthenticationResultDto): PayloadAction<string, AuthenticationResultDto> {
    return action(UserActionTypes.LOGIN_FAILED, dto);
}