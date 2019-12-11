import { createAppState } from '../../../testing';
import { ShellActions } from '../actions';
import { selectIsSidenavOpen } from './shell.selectors';

describe('shellSelectors', () => {
  it('should have open sidenav', () => {
    const state = createAppState(ShellActions.toggleSidenav());

    expect(selectIsSidenavOpen(state)).toEqual(true);
  });

  it('should have closed sidenav', () => {
    const state = createAppState(ShellActions.closeSidenav());

    expect(selectIsSidenavOpen(state)).toEqual(false);
  });
});
