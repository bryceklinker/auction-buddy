import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AppState } from '../../../root.reducer';
import { ShellActions } from '../../actions';
import { selectIsSidenavOpen } from '../../reducers';

@Component({
  selector: 'app-root',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent {
  isSidenavOpen$: Observable<boolean>;

  constructor(private store: Store<AppState>) {
    this.isSidenavOpen$ = store.select(selectIsSidenavOpen);
  }

  toggleSidenav() {
    this.store.dispatch(ShellActions.toggleSidenav());
  }

  closeSidenav() {
    this.store.dispatch(ShellActions.closeSidenav());
  }
}
