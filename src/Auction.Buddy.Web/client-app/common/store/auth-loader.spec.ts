import { createTestingStore } from '../../testing/create-testing-store';
import { loadAuth } from './auth-loader';
import { createSuccessAuthenticationResultDto } from '../../testing/dto-factory';
import { saveAuth } from './auth-storage';
import { loginSuccessAction, UserActionTypes } from './actions/user-actions';

describe('authLoader', () => {
    it('should dispatch successful auth result action', () => {
        const dto = createSuccessAuthenticationResultDto();
        saveAuth(dto);

        const store = createTestingStore();
        loadAuth(store);

        expect(store.getActions()).toContainEqual(loginSuccessAction(dto));
    });

    it('should not dispatch successful auth result', () => {
        const store = createTestingStore();
        loadAuth(store);

        expect(store.getActions()).not.toContainEqual(
            expect.objectContaining({
                type: UserActionTypes.LOGIN_SUCCESS,
            }),
        );
    });

    afterEach(() => localStorage.clear());
});
