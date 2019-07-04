import * as React from "react";
import {Provider} from 'react-redux';
import {Store} from "redux";
import {BrowserRouter, Route} from "react-router-dom";

import {AppState} from "../../app-state";
import {Header} from "./Header";
import {MainContent} from "./MainContent";
import {Login} from "../../login/components/Login";

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
                        <Route exact path={'/'} component={Login} />
                    </BrowserRouter>
                </MainContent>
            </div>
        </Provider>
    )
}