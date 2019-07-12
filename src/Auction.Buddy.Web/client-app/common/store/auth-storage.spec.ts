import { createFailedAuthenticationResultDto, createSuccessAuthenticationResultDto } from '../../testing/dto-factory';
import { getAuth, saveAuth } from './auth-storage';

describe('authStorage', () => {
    describe('saveAuth', () => {
        it('should save authentication when auth is success to storage', () => {
            const dto = createSuccessAuthenticationResultDto();

            saveAuth(dto);
            expect(JSON.parse(localStorage.getItem('AUTH_DTO') || '{}')).toEqual(dto);
        });

        it('should clear authentication when auth is unsuccessful', () => {
            saveAuth(createSuccessAuthenticationResultDto());
            saveAuth(createFailedAuthenticationResultDto());

            expect(localStorage.getItem('AUTH_DTO')).toEqual(null);
        });
    });

    describe('getAuth', () => {
        it('should return previously saved authentication', () => {
            const dto = createSuccessAuthenticationResultDto();
            localStorage.setItem('AUTH_DTO', JSON.stringify(dto));

            const actual = getAuth();
            expect(actual).toEqual(dto);
        });

        it('should return null when authentication does not exist', () => {
            localStorage.removeItem('AUTH_DTO');

            expect(getAuth()).toEqual(null);
        });
    });

    afterEach(() => {
        localStorage.clear();
    });
});
