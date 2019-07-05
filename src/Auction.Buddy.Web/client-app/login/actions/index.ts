import {action, PayloadAction} from 'typesafe-actions';
import {CredentialsDto} from "../dtos/credentials-dto";
import {AuthenticationResultDto} from "../dtos/authentication-result-dto";

export const LoginActionTypes = {
    LOGIN_REQUEST: '[Login] Request',
    LOGIN_SUCCESS: '[Login] Success',
    LOGIN_FAILED: '[Login] Failed'
};

export function loginRequestAction(dto: CredentialsDto): PayloadAction<string, CredentialsDto> {
    return action(LoginActionTypes.LOGIN_REQUEST, dto);
}

export function loginSuccessAction(dto: AuthenticationResultDto): PayloadAction<string, AuthenticationResultDto> {
    return action(LoginActionTypes.LOGIN_SUCCESS, dto);
}

export function loginFailedAction(dto: AuthenticationResultDto): PayloadAction<string, AuthenticationResultDto> {
    return action(LoginActionTypes.LOGIN_FAILED, dto);
}