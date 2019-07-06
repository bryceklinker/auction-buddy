import {RouterState} from 'connected-react-router';

import {UserState} from "./common/store/reducers/user-reducer";
import {AuctionsState} from "./common/store/reducers/auctions-reducer";

export interface AppState {
    user: UserState;
    auctions: AuctionsState;
    router: RouterState;
}