import * as React from "react";
import { useState } from 'react';
import {CredentialsDto} from "../../common/store/dtos/credentials-dto";

interface Props {
    hasLoginFailed: boolean;
    isLoggingIn: boolean;
    onLogin: (credentials: CredentialsDto) => void;
}

export function Login({ onLogin, hasLoginFailed, isLoggingIn }: Props) {
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
            <button data-testid="login-button" disabled={isLoggingIn} onClick={handleLoginButtonClick}>Login</button>
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