import { createTestingStore } from '../../testing/create-testing-store';
import { loadAuth } from './auth-loader';
import { createSuccessAuthenticationResultDto } from '../../testing/dto-factory';
import { getAuth, saveAuth } from './auth-storage';
import { loginSuccessAction, UserActionTypes } from './actions/user-actions';

describe('authLoader', () => {
    it('should dispatch successful auth result action', () => {
        const dto = createSuccessAuthenticationResultDto();
        saveAuth(dto);

        const store = createTestingStore();
        loadAuth(store);

        expect(store.getActions()).toContainEqual(loginSuccessAction(dto));
    });

    it('should not dispatch successful auth result when no auth has been saved', () => {
        const store = createTestingStore();
        loadAuth(store);

        expect(store.getActions()).not.toContainEqual(
            expect.objectContaining({
                type: UserActionTypes.LOGIN_SUCCESS,
            }),
        );
    });

    it('should not dispatch successful auth result when auth has expired', () => {
        const dto = createSuccessAuthenticationResultDto();
        dto.expiresAt = '1970-01-01T00:00:00.000Z';

        const store = createTestingStore();
        saveAuth(dto);

        loadAuth(store);
        expect(store.getActions()).not.toContainEqual(
            expect.objectContaining({
                type: UserActionTypes.LOGIN_SUCCESS,
            }),
        );
    });

    it('should clear auth result when auth has expired', () => {
        const dto = createSuccessAuthenticationResultDto();
        dto.expiresAt = '1970-01-01T00:00:00.000Z';

        const store = createTestingStore();
        saveAuth(dto);

        loadAuth(store);

        expect(getAuth()).toEqual(null);
    });

    afterEach(() => localStorage.clear());
});
