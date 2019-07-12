import { AuthenticationResultDto } from './dtos/authentication-result-dto';

const AUTH_DTO_KEY = 'AUTH_DTO';
export function saveAuth(dto: AuthenticationResultDto) {
    if (dto.isSuccess) {
        const json = JSON.stringify(dto);
        localStorage.setItem(AUTH_DTO_KEY, json);
    } else {
        localStorage.removeItem(AUTH_DTO_KEY);
    }
}

export function getAuth(): AuthenticationResultDto | null {
    const json = localStorage.getItem(AUTH_DTO_KEY);
    if (json) {
        return JSON.parse(json);
    }

    return null;
}
