import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { Store } from '@ngrx/store';
import { provideMockStore } from '@ngrx/store/testing';
import { MaterialModule } from '../app/material.module';
import { AppState } from '../app/root.reducer';
import { CapturingStore } from './capturing-store';
import { createAppState } from './create-app-state';

export function createTestingModule(state: AppState, module: any = null) {
  return {
    imports: [
      ...(module ? [module] : []),
      HttpClientTestingModule,
      RouterTestingModule.withRoutes([]),
      NoopAnimationsModule,
      MaterialModule
    ],
    providers: [
      ...provideMockStore({ initialState: state ? state : createAppState() }),
      { provide: Store, useClass: CapturingStore }
    ]
  };
}
