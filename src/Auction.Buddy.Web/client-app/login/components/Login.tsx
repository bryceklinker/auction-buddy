import * as React from 'react';
import { useState } from 'react';
import { CredentialsDto } from '../../common/store/dtos/credentials-dto';
import { AppState } from '../../app-state';
import { hasLoginFailedSelector } from '../../common/store/reducers/user-reducer';
import { isUserLoggingInSelector } from '../../common/store/reducers/loading-reducer';
import { Action, Dispatch } from 'redux';
import { loginRequestAction } from '../../common/store/actions/user-actions';
import { connect } from 'react-redux';

interface Props {
    hasLoginFailed: boolean;
    isLoggingIn: boolean;
    onLogin: (credentials: CredentialsDto) => void;
}

function LoginView({ onLogin, hasLoginFailed, isLoggingIn }: Props) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleLoginButtonClick = () => {
        onLogin({ username, password });
    };

    return (
        <div>
            {hasLoginFailed ? <LoginErrorMessage /> : null}
            <input data-testid="username-input" value={username} onChange={t => setUsername(t.target.value)} />
            <input
                data-testid="password-input"
                type={'password'}
                value={password}
                onChange={t => setPassword(t.target.value)}
            />
            <button data-testid="login-button" disabled={isLoggingIn} onClick={handleLoginButtonClick}>
                Login
            </button>
        </div>
    );
}

function LoginErrorMessage() {
    return <div data-testid="login-error">Invalid username or password</div>;
}

function mapStateToProps(state: AppState) {
    return {
        hasLoginFailed: hasLoginFailedSelector(state),
        isLoggingIn: isUserLoggingInSelector(state),
    };
}

function mapDispatchToProps(dispatch: Dispatch<Action>) {
    return {
        onLogin: (credentials: CredentialsDto) => dispatch(loginRequestAction(credentials)),
    };
}

export const Login = connect(
    mapStateToProps,
    mapDispatchToProps,
)(LoginView);
