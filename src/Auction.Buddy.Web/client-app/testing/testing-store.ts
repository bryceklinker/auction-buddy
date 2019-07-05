import createMockStore, {MockStore} from 'redux-mock-store';
import {Action} from "redux";
import {AppState} from "../app-state";
import {createRootReducer} from "../common/store/create-root-reducer";

export function createState(...actions: Action[]): AppState {
    const reducer = createRootReducer();
    return actions.reduce((state: AppState, action: Action): AppState => reducer(state, action), reducer(undefined, { type: '@@init' }));
}

export function createTestingStore(...actions: Action[]): MockStore<AppState> {
    const state = createState(...actions);
    return createMockStore<AppState>([])(state);
}