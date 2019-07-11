import { applyMiddleware, createStore, Store } from 'redux';
import createSagaMiddleware from 'redux-saga';
import { History } from 'history';
import { routerMiddleware } from 'connected-react-router';

import { AppState } from '../../app-state';
import { createRootReducer } from './create-root-reducer';
import { rootSaga } from './root-saga';
import { composeWithDevTools } from 'redux-devtools-extension';

export function configureStore(history: History): Store<AppState> {
    const sagaMiddleware = createSagaMiddleware();
    const reducer = createRootReducer(history);

    const store = createStore(reducer, composeWithDevTools(applyMiddleware(sagaMiddleware, routerMiddleware(history))));
    sagaMiddleware.run(rootSaga);
    return store;
}
