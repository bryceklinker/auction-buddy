import { Action } from 'redux';
import { initAction } from '../actions/init-actions';
import { AppState } from '../../../app-state';
import { createSelector } from 'reselect';
import { AuctionActionTypes } from '../actions/auction-actions';
import { UserActionTypes } from '../actions/user-actions';

const ACTION_CONVENTION_REGEX = /(.*)\s(Request|Success|Failed)/;

export interface LoadingState {
    [key: string]: boolean;
}

const initialState: LoadingState = {};

export function loadingReducer(
    state: LoadingState = initialState,
    action: Action<string> = initAction(),
): LoadingState {
    const matches = ACTION_CONVENTION_REGEX.exec(action.type);
    if (!matches) return state;

    const [, requestName, requestState] = matches;

    return {
        ...state,
        [requestName]: requestState === 'Request',
    };
}

function selectLoadingState(state: AppState): LoadingState {
    return state.loading;
}

function getRequestName(type: string): string {
    const matches = ACTION_CONVENTION_REGEX.exec(type);
    if (!matches) return '';

    const [, requestName] = matches;
    return requestName;
}

function getIsLoading(actionType: string, state: LoadingState): boolean {
    const requestName = getRequestName(actionType);
    return state[requestName];
}

export const isLoadingAuctionsSelector = createSelector(
    selectLoadingState,
    s => getIsLoading(AuctionActionTypes.GET_ALL_REQUEST, s),
);
export const isCreatingAuctionSelector = createSelector(
    selectLoadingState,
    s => getIsLoading(AuctionActionTypes.CREATE_REQUEST, s),
);
export const isUserLoggingInSelector = createSelector(
    selectLoadingState,
    s => getIsLoading(UserActionTypes.LOGIN_REQUEST, s),
);
