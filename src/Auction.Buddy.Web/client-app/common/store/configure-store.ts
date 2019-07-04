import {applyMiddleware, createStore, Store} from "redux";
import createSagaMiddleware from 'redux-saga';

import {AppState} from "../../app-state";
import {createRootReducer} from "./create-root-reducer";
import {rootSaga} from "./root-saga";

export function configureStore(): Store<AppState> {
    const sagaMiddleware = createSagaMiddleware(); 
    const reducer = createRootReducer();
    
    const store = createStore(reducer, applyMiddleware(
        sagaMiddleware
    ));
    sagaMiddleware.run(rootSaga);
    return store;
}