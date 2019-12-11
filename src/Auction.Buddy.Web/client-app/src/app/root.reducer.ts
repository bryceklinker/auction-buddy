import {ShellState} from "./shell/reducers";
import {ActionReducerMap} from "@ngrx/store";
import { shellReducer } from './shell/reducers/shell.reducer';

export interface AppState {
  shell: ShellState
}

export const rootReducers: ActionReducerMap<AppState> = {
  shell: shellReducer
};
