import { combineReducers, Reducer } from 'redux';
import { History } from 'history';
import { connectRouter } from 'connected-react-router';

import { AppState } from '../../app-state';
import { userReducer } from './reducers/user-reducer';
import { auctionsReducer } from './reducers/auctions-reducer';
import { loadingReducer } from './reducers/loading-reducer';

export function createRootReducer(history: History): Reducer<AppState> {
    return combineReducers({
        user: userReducer,
        auctions: auctionsReducer,
        loading: loadingReducer,
        router: connectRouter(history),
    });
}
