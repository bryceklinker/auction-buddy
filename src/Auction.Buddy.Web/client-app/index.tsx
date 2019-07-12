import * as React from 'react';
import { render } from 'react-dom';

import { Shell } from './shell/components/Shell';
import { configureStore } from './common/store/configure-store';
import { createBrowserHistory } from 'history';
import { loadAuth } from './common/store/auth-loader';

import './index.scss';
const history = createBrowserHistory();
const store = configureStore(history);
loadAuth(store);
render(<Shell store={store} history={history} />, document.getElementById('root'));
