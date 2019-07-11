import { Store } from 'redux';
import { AppState } from '../app-state';
import { render, RenderResult } from '@testing-library/react';
import { Provider } from 'react-redux';
import * as React from 'react';
import { createTestingStore } from './create-testing-store';

export function renderWithStore(Component: JSX.Element, store: Store<AppState> = createTestingStore()): RenderResult {
    return render(<Provider store={store}>{Component}</Provider>);
}
