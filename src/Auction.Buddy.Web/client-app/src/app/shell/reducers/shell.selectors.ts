import { createSelector } from '@ngrx/store';
import { AppState } from '../../root.reducer';
import { ShellState } from './shell.state';

export const selectIsSidenavOpen =
  createSelector<AppState, ShellState, boolean>(s => s.shell, s => s.isSidenavOpen);
