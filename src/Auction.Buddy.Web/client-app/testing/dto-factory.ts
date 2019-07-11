import { AuthenticationResultDto } from '../common/store/dtos/authentication-result-dto';
import * as faker from 'faker';

export function createSuccessAuthenticationResult(): AuthenticationResultDto {
    return {
        isSuccess: true,
        tokenType: 'Bearer',
        accessToken: `${faker.hacker.verb()}.${faker.hacker.noun()}.${faker.random.alphaNumeric(12)}`,
        expiresIn: faker.random.number(10000),
    };
}
