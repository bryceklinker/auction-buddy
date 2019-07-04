import {combineReducers, Reducer} from "redux";

import {AppState} from "../../app-state";

export function createRootReducer(): Reducer<AppState> {
    return combineReducers({
        data: () => ({})
    });
}