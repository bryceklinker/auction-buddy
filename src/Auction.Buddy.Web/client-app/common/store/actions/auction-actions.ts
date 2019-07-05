import {AuctionDto, CreateAuctionDto} from "../dtos/auction-dto";
import {action} from "typesafe-actions";
import {ValidationResultDto} from "../dtos/validation-result-dto";

export const AuctionActionTypes = {
    CREATE_REQUEST: '[Auctions] Create Auction Request',
    CREATE_SUCCESS: '[Auctions] Create Auction Success',
    CREATE_FAILED: '[Auctions] Create Auction Failed'
};

export function createAuctionRequestAction(dto: CreateAuctionDto) {
    return action(AuctionActionTypes.CREATE_REQUEST, dto);
}

export function createAuctionSuccessAction(dto: AuctionDto) {
    return action(AuctionActionTypes.CREATE_SUCCESS, dto);
}

export function createAuctionFailedAction(dto: ValidationResultDto) {
    return action(AuctionActionTypes.CREATE_FAILED, dto);
}