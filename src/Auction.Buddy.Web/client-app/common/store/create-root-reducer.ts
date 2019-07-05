import {combineReducers, Reducer} from "redux";

import {AppState} from "../../app-state";
import {userReducer} from "./reducers/user-reducer";

export function createRootReducer(): Reducer<AppState> {
    return combineReducers({
        user: userReducer
    });
}