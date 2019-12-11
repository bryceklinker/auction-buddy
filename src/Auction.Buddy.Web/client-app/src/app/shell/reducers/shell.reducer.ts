import { ActionReducer } from '@ngrx/store';
import { ShellActionTypes } from '../actions';
import { ShellState } from './shell.state';

const initialState: ShellState = {
  isSidenavOpen: false
};

export const shellReducer: ActionReducer<ShellState> = (state = initialState, action) => {
  switch (action.type) {
    case ShellActionTypes.TOGGLE_SIDENAV:
      return { ...state, isSidenavOpen: !state.isSidenavOpen };
    case ShellActionTypes.CLOSE_SIDENAV:
      return { ...state, isSidenavOpen: false };
    default:
      return state;
  }
};
