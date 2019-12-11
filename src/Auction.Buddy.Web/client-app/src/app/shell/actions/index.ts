import {createAction} from '@ngrx/store'

export const ShellActionTypes = {
  TOGGLE_SIDENAV: '[Shell] Toggle Sidenav',
  CLOSE_SIDENAV: '[Shell] Close Sidenav'
};

const toggleSidenav = createAction(ShellActionTypes.TOGGLE_SIDENAV);
const closeSidenav = createAction(ShellActionTypes.CLOSE_SIDENAV);

export const ShellActions = {
  toggleSidenav,
  closeSidenav
};
