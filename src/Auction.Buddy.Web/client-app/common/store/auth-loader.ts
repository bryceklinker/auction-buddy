import { Store } from 'redux';

import { AppState } from '../../app-state';
import { getAuth, clearAuth } from './auth-storage';
import { loginSuccessAction } from './actions/user-actions';
import { AuthenticationResultDto } from './dtos/authentication-result-dto';

function isAuthenticationValid(authDto: AuthenticationResultDto | null) {
    return authDto && authDto.expiresAt && Date.parse(authDto.expiresAt) > Date.now();
}

export function loadAuth(store: Store<AppState>) {
    const authDto = getAuth();
    if (isAuthenticationValid(authDto)) {
        store.dispatch(loginSuccessAction(authDto as AuthenticationResultDto));
    } else {
        clearAuth();
    }
}
