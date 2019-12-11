import { initAction } from '../../../testing';
import { ShellActions } from '../actions';
import { shellReducer } from './shell.reducer';


describe('shellReducer', () => {
  it('should have default state', () => {
    const state = shellReducer(undefined, initAction());

    expect(state.isSidenavOpen).toEqual(false);
  });

  it('should have open sidenav when currently closed and sidenav is toggled', () => {
    let state = shellReducer(undefined, initAction());

    state = shellReducer(state, ShellActions.toggleSidenav());

    expect(state.isSidenavOpen).toEqual(true);
  });

  it('should have closed sidenav when currently open and sidenav is toggled', () => {
    let state = shellReducer(undefined, initAction());
    state = shellReducer(state, ShellActions.toggleSidenav());

    state = shellReducer(state, ShellActions.toggleSidenav());

    expect(state.isSidenavOpen).toEqual(false);
  });

  it('should close sidenav when currently open and sidenav is closed', () => {
    let state = shellReducer(undefined, initAction());
    state = shellReducer(state, ShellActions.toggleSidenav());

    state = shellReducer(state, ShellActions.closeSidenav());

    expect(state.isSidenavOpen).toEqual(false);
  });
});
