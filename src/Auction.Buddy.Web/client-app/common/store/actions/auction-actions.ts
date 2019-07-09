import {AuctionDto, CreateAuctionDto} from "../dtos/auction-dto";
import {action, PayloadAction} from "typesafe-actions";
import {ValidationResultDto} from "../dtos/validation-result-dto";
import {InitAction} from "./init-actions";

export const AuctionActionTypes = {
    CREATE_REQUEST: '[Auctions] Create Auction Request',
    CREATE_SUCCESS: '[Auctions] Create Auction Success',
    CREATE_FAILED: '[Auctions] Create Auction Failed'
};

export interface CreateAuctionRequestAction extends PayloadAction<string, CreateAuctionDto> {
    type: typeof AuctionActionTypes.CREATE_REQUEST;
}

export function createAuctionRequestAction(dto: CreateAuctionDto): CreateAuctionRequestAction {
    return action(AuctionActionTypes.CREATE_REQUEST, dto);
}

export interface CreateAuctionSuccessAction extends PayloadAction<string, AuctionDto> {
    type: typeof AuctionActionTypes.CREATE_SUCCESS;
}

export function createAuctionSuccessAction(dto: AuctionDto): CreateAuctionSuccessAction {
    return action(AuctionActionTypes.CREATE_SUCCESS, dto);
}

export interface CreateAuctionFailedAction extends PayloadAction<string, ValidationResultDto> {
    type: typeof AuctionActionTypes.CREATE_FAILED;
}

export function createAuctionFailedAction(dto: ValidationResultDto): CreateAuctionFailedAction {
    return action(AuctionActionTypes.CREATE_FAILED, dto);
}

export type AuctionActions = CreateAuctionRequestAction | CreateAuctionSuccessAction | CreateAuctionFailedAction | InitAction;