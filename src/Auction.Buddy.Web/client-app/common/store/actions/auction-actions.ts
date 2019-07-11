import { AuctionDto, CreateAuctionDto } from '../dtos/auction-dto';
import { action, PayloadAction } from 'typesafe-actions';
import { ValidationResultDto } from '../dtos/validation-result-dto';
import { InitAction } from './init-actions';

export const AuctionActionTypes = {
    CREATE_REQUEST: '[Auctions] Create Auction Request',
    CREATE_SUCCESS: '[Auctions] Create Auction Success',
    CREATE_FAILED: '[Auctions] Create Auction Failed',

    GET_ALL_REQUEST: '[Auctions] Get All Auctions Request',
    GET_ALL_SUCCESS: '[Auctions] Get All Auctions Success',
    GET_ALL_FAILED: '[Auctions] Get All Auctions Failed',
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

export interface GetAllAuctionsRequestAction extends PayloadAction<string, null> {
    type: typeof AuctionActionTypes.GET_ALL_REQUEST;
}

export function getAllAuctionsRequestAction(): GetAllAuctionsRequestAction {
    return action(AuctionActionTypes.GET_ALL_REQUEST, null);
}

export interface GetAllAuctionsSuccessAction extends PayloadAction<string, AuctionDto[]> {
    type: typeof AuctionActionTypes.GET_ALL_SUCCESS;
}

export function getAllAuctionsSuccessAction(dtos: AuctionDto[]): GetAllAuctionsSuccessAction {
    return action(AuctionActionTypes.GET_ALL_SUCCESS, dtos);
}

export interface GetAllAuctionsFailedAction extends PayloadAction<string, string> {
    type: typeof AuctionActionTypes.GET_ALL_FAILED;
}

export function getAllAuctionsFailedAction(error: string): GetAllAuctionsFailedAction {
    return action(AuctionActionTypes.GET_ALL_FAILED, error);
}

export type AuctionActions =
    | CreateAuctionRequestAction
    | CreateAuctionSuccessAction
    | CreateAuctionFailedAction
    | GetAllAuctionsRequestAction
    | GetAllAuctionsSuccessAction
    | GetAllAuctionsFailedAction
    | InitAction;
