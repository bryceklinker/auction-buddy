import * as React from "react";
import { useState } from 'react';
import {History} from "history";
import {CredentialsDto} from "../../common/store/dtos/credentials-dto";

interface Props {
    history: History;
    isUserLoggedIn: boolean;
    hasLoginFailed: boolean;
    onLogin: (credentials: CredentialsDto) => void;
}

export function Login(props: Props) {
    return props.isUserLoggedIn ? <LoginRedirect {...props} /> : <LoginForm {...props} />
}

function LoginRedirect({ history }: Props) {
    history.push('/auctions');
    return (
        <div />
    )
}

function LoginForm({ onLogin, hasLoginFailed }: Props) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleLoginButtonClick = () => {
        onLogin({ username, password });
    };
    
    return (
        <div>
            {hasLoginFailed ? <LoginErrorMessage /> : null}
            <input data-testid="username-input" value={username} onChange={t => setUsername(t.target.value)} />
            <input data-testid="password-input" type={'password'} value={password} onChange={t => setPassword(t.target.value)} />
            <button data-testid="login-button" onClick={handleLoginButtonClick}>Login</button>
        </div>
    )
}

function LoginErrorMessage() {
    return (
        <div data-testid="login-error">
            Invalid username or password
        </div>
    )
}