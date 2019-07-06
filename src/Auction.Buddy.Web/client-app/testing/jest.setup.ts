import {GlobalWithFetchMock} from "jest-fetch-mock";

import 'jest-dom/extend-expect';

const customGlobal: GlobalWithFetchMock = global as GlobalWithFetchMock;
customGlobal.fetch = require('jest-fetch-mock');
customGlobal.fetchMock = customGlobal.fetch;