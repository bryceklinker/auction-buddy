import { TestBed } from '@angular/core/testing';
import { Action, Store } from '@ngrx/store';
import { render, RenderResult } from '@testing-library/angular';
import { Type } from '@angular/core';
import { AppState } from '../app/root.reducer';
import { CapturingStore } from './capturing-store';
import { createAppState } from './create-app-state';
import { createTestingModule } from './create-testing-module';

interface StoreRenderResult<ComponentType> extends RenderResult<ComponentType, ComponentType> {
  store: CapturingStore<AppState>
}

export async function renderWithStore<TComponent>(component: Type<TComponent>, ...actions: Action[]): Promise<StoreRenderResult<TComponent>>  {
  const state = createAppState(...actions);
  const result = await render(component, createTestingModule(state));
  const store = TestBed.get(Store);
  return {...result, store};
}
