import createMockStore, {MockStore} from 'redux-mock-store';
import {Action} from "redux";
import {AppState} from "../app-state";
import {createRootReducer} from "../common/store/create-root-reducer";
import {createMemoryHistory, History} from "history";

export function createState(history: History = createMemoryHistory(), ...actions: Action[]): AppState {
    const reducer = createRootReducer(history);
    return actions.reduce((state: AppState, action: Action): AppState => reducer(state, action), reducer(undefined, { type: '@@init' }));
}

export function createTestingStore(history: History = createMemoryHistory(), ...actions: Action[]): MockStore<AppState> {
    const state = createState(history, ...actions);
    return createMockStore<AppState>([])(state);
}