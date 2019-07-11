import {RouterState} from 'connected-react-router';

import {UserState} from "./common/store/reducers/user-reducer";
import {AuctionsState} from "./common/store/reducers/auctions-reducer";
import {LoadingState} from "./common/store/reducers/loading-reducer";

export interface AppState {
    user: UserState;
    auctions: AuctionsState;
    loading: LoadingState;
    router: RouterState;
}