import { Action, combineReducers } from '@ngrx/store';
import { AppState, rootReducers } from '../app/root.reducer';
import { initAction } from './actions';

export function createAppState(...actions: Action[]) {
  const reducer = combineReducers<AppState>(rootReducers);
  const initialState = reducer(undefined, initAction);
  return actions.reduce((state, action) => reducer(state, action), initialState);
}
