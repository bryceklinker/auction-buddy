import { Store } from 'redux';

import { AppState } from '../../app-state';
import { getAuth } from './auth-storage';
import { loginSuccessAction } from './actions/user-actions';

export function loadAuth(store: Store<AppState>) {
    const authDto = getAuth();
    if (authDto) {
        store.dispatch(loginSuccessAction(authDto));
    }
}
