import {AuthenticationResultDto} from "../dtos/authentication-result-dto";
import {loginRequestAction} from "../actions";
import {CredentialsDto} from "../dtos/credentials-dto";
import {performLogin} from "./login-saga";
import SagaTester from 'redux-saga-tester';
import {AppState} from "../../app-state";

describe('loginSaga', () => {
    let tester: SagaTester<AppState>;

    beforeEach(() => {
        tester = new SagaTester<AppState>();
    });

    it('should post credentials to api', async () => {
        const result: AuthenticationResultDto = {
            isSuccess: true,
            accessToken: 'header.payload.signature',
            expiresIn: 100,
            tokenType: 'Bearer'
        };
        fetchMock.mockResponse(JSON.stringify(result));

        const credentials: CredentialsDto = {username: 'bob', password: 'jim'};
        await tester.start(performLogin, loginRequestAction(credentials)).done;
        expect(fetch).toHaveBeenCalledWith('/authentication/login');
    })
});