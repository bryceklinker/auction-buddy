import * as React from "react";
import {Provider} from 'react-redux';
import {Store} from "redux";
import {BrowserRouter, Route} from "react-router-dom";

import {AppState} from "../../app-state";
import {Header} from "./Header";
import {MainContent} from "./MainContent";
import {LoginContainer} from "../../login/containers/LoginContainer";
import {AuctionsListContainer} from "../../auctions/containers/AuctionsListContainer";
import {CreateAuctionContainer} from "../../auctions/containers/CreateAuctionContainer";

interface Props {
    store: Store<AppState>;
}

export function Shell({store}: Props) {
    return (
        <Provider store={store}>
            <div>
                <Header />
                <MainContent>
                    <BrowserRouter>
                        <Route exact path={'/'} component={LoginContainer} />
                        <Route path={'/auctions'} component={AuctionsListContainer} />
                        <Route path={'/create-auction'} component={CreateAuctionContainer} />
                    </BrowserRouter>
                </MainContent>
            </div>
        </Provider>
    )
}