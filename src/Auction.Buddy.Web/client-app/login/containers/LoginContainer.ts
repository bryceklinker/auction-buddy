import {connect} from "react-redux";
import {Login} from "../components/Login";
import {Action, Dispatch} from "redux";
import {AppState} from "../../app-state";
import {hasLoginFailedSelector, isUserLoggedInSelector} from "../../common/store/reducers/userReducer";
import {CredentialsDto} from "../../common/store/dtos/credentials-dto";
import {loginRequestAction} from "../../common/store/actions/user-actions";

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