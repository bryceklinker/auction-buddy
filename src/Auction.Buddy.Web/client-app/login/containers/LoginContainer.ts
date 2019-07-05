import {connect} from "react-redux";
import {Action, Dispatch} from "redux";

import {Login} from "../components/Login";
import {AppState} from "../../app-state";
import {CredentialsDto} from "../../common/store/dtos/credentials-dto";
import {loginRequestAction} from "../../common/store/actions/user-actions";
import {hasLoginFailedSelector, isUserLoggedInSelector} from "../../common/store/reducers/user-reducer";

function mapStateToProps(state: AppState) {
    return {
        hasLoginFailed: hasLoginFailedSelector(state),
        isUserLoggedIn: isUserLoggedInSelector(state)
    }
}

function mapDispatchToProps(dispatch: Dispatch<Action>) {
    return {
        onLogin: (credentials: CredentialsDto) => dispatch(loginRequestAction(credentials))
    };
}

export const LoginContainer = connect(mapStateToProps, mapDispatchToProps)(Login);