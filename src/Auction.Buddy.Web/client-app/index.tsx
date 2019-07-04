import * as React from "react";
import { render } from 'react-dom';

import {Shell} from './shell/components/Shell';
import {configureStore} from "./common/store/configure-store";

const store = configureStore();
render(
    <Shell store={store} />,
    document.getElementById("root")
);