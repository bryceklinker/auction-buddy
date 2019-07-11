import { api, ApiService } from './api';
import { AuthenticationResultDto } from '../store/dtos/authentication-result-dto';

export function createAuthApi(authentication: AuthenticationResultDto): ApiService {
    const { tokenType, accessToken } = authentication;
    const authHeaderValue = `${tokenType} ${accessToken}`;
    return {
        post: <TBody, TResult>(url: string, data: TBody, headers: HeadersInit = {}): Promise<TResult> => {
            const authHeaders = {
                Authorization: authHeaderValue,
                ...headers,
            };
            return api.post<TBody, TResult>(url, data, authHeaders);
        },
        get: <TResult>(url: string, headers: HeadersInit = {}): Promise<TResult> => {
            const authHeaders = {
                Authorization: authHeaderValue,
                ...headers,
            };
            return api.get(url, authHeaders);
        },
    };
}
