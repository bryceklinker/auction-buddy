import * as faker from 'faker';

import { AuthenticationResultDto } from '../common/store/dtos/authentication-result-dto';
import { AuctionDto } from '../common/store/dtos/auction-dto';

export function createSuccessAuthenticationResultDto(): AuthenticationResultDto {
    return {
        isSuccess: true,
        tokenType: 'Bearer',
        accessToken: `${faker.hacker.verb()}.${faker.hacker.noun()}.${faker.random.alphaNumeric(12)}`,
        expiresIn: faker.random.number(10000),
    };
}

export function createAuctionDto(): AuctionDto {
    return {
        id: faker.random.number(),
        auctionDate: faker.date.future().toISOString(),
        name: faker.name.title(),
    };
}
