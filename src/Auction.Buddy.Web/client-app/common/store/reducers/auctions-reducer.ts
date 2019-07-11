import { AuctionDto } from '../dtos/auction-dto';
import { initAction } from '../actions/init-actions';
import {
    AuctionActions,
    AuctionActionTypes,
    CreateAuctionFailedAction,
    CreateAuctionSuccessAction,
    GetAllAuctionsSuccessAction,
} from '../actions/auction-actions';
import { ValidationResultDto } from '../dtos/validation-result-dto';
import { AppState } from '../../../app-state';
import { createSelector } from 'reselect';

export interface AuctionsState {
    auctions: AuctionDto[];
    validationResult: ValidationResultDto | null;
}

const initialState: AuctionsState = {
    auctions: [],
    validationResult: null,
};

export function auctionsReducer(
    state: AuctionsState = initialState,
    action: AuctionActions = initAction(),
): AuctionsState {
    switch (action.type) {
        case AuctionActionTypes.CREATE_REQUEST:
            return {
                ...state,
                validationResult: null,
            };

        case AuctionActionTypes.CREATE_SUCCESS:
            return {
                ...state,
                auctions: [...state.auctions, (action as CreateAuctionSuccessAction).payload],
            };

        case AuctionActionTypes.CREATE_FAILED:
            return {
                ...state,
                validationResult: (action as CreateAuctionFailedAction).payload,
            };

        case AuctionActionTypes.GET_ALL_SUCCESS:
            return {
                ...state,
                auctions: (action as GetAllAuctionsSuccessAction).payload,
            };

        default:
            return state;
    }
}

function selectAuctionsState(state: AppState): AuctionsState {
    return state.auctions;
}

function selectAuctionId(_: AppState, id: string): number {
    return Number(id);
}

export const auctionsSelector = createSelector(
    selectAuctionsState,
    s => s.auctions,
);
export const auctionDetailSelector = createSelector(
    [auctionsSelector, selectAuctionId],
    (auctions: AuctionDto[], id: number) => auctions.find(a => a.id === id),
);
export const auctionsValidationResultSelector = createSelector(
    selectAuctionsState,
    s => s.validationResult,
);
