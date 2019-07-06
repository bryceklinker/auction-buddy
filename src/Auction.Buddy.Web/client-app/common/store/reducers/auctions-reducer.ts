import {Action} from "redux";
import {AuctionDto} from "../dtos/auction-dto";
import {initAction} from "../actions/init-actions";
import {AuctionActionTypes} from "../actions/auction-actions";
import {PayloadAction} from "typesafe-actions";
import {ValidationResultDto} from "../dtos/validation-result-dto";
import {AppState} from "../../../app-state";
import {createSelector} from "reselect";

export interface AuctionsState {
    auctions: AuctionDto[];
    isCreating: boolean;
    createError: ValidationResultDto | null;
}

const initialState: AuctionsState = {
    auctions: [],
    isCreating: false,
    createError: null
};

export function auctionsReducer(state: AuctionsState = initialState, action: Action = initAction()): AuctionsState {
    switch (action.type) {
        case AuctionActionTypes.CREATE_REQUEST:
            return {...state, isCreating: true };
        
        case AuctionActionTypes.CREATE_SUCCESS:
            return {
                ...state, 
                auctions: [...state.auctions, (action as PayloadAction<string, AuctionDto>).payload], 
                isCreating: false 
            };
            
        case AuctionActionTypes.CREATE_FAILED:
            return {
                ...state,
                createError: (action as PayloadAction<string, ValidationResultDto>).payload,
                isCreating: false
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

export const isCreatingAuctionSelector = createSelector(selectAuctionsState, s => s.isCreating);
export const auctionsSelector = createSelector(selectAuctionsState, s => s.auctions);
export const auctionDetailSelector = createSelector([auctionsSelector, selectAuctionId], (auctions: AuctionDto[], id: number) => auctions.find(a => a.id === id));