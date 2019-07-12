import * as React from 'react';
import { FormEvent, useState } from 'react';
import { connect } from 'react-redux';
import { Button, makeStyles } from '@material-ui/core';
import { Action, Dispatch } from 'redux';

import { CredentialsDto } from '../../common/store/dtos/credentials-dto';
import { AppState } from '../../app-state';
import { hasLoginFailedSelector } from '../../common/store/reducers/user-reducer';
import { isUserLoggingInSelector } from '../../common/store/reducers/loading-reducer';
import { loginRequestAction } from '../../common/store/actions/user-actions';
import { InputField } from '../../common/components/InputField';
import { LoginErrorMessage } from './LoginErrorMessage';

const useStyles = makeStyles(theme => ({
    container: {
        display: 'flex',
        flexWrap: 'wrap',
        alignSelf: 'center',
        justifyContent: 'center',
        margin: theme.spacing(1),
    },
    loginButton: {
        marginTop: theme.spacing(2),
    },
}));

interface Props {
    hasLoginFailed: boolean;
    isLoggingIn: boolean;
    onLogin: (credentials: CredentialsDto) => void;
}

function LoginView({ onLogin, hasLoginFailed, isLoggingIn }: Props) {
    const classes = useStyles();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleLoginButtonClick = (evt: FormEvent) => {
        if (evt) {
            evt.preventDefault();
        }

        onLogin({ username, password });
    };

    return (
        <form className={classes.container} onSubmit={handleLoginButtonClick}>
            <LoginErrorMessage hasLoginFailed={hasLoginFailed} />
            <InputField
                label={'Username'}
                value={username}
                onChange={t => setUsername(t.target.value)}
                inputProps={{ 'data-testid': 'username-input' }}
                fullWidth
            />
            <InputField
                label={'Password'}
                value={password}
                onChange={t => setPassword(t.target.value)}
                inputProps={{ 'data-testid': 'password-input' }}
                type={'password'}
                fullWidth
            />
            <Button
                data-testid={'login-button'}
                disabled={isLoggingIn}
                type={'submit'}
                variant={'contained'}
                color={'primary'}
                className={classes.loginButton}
            >
                Login
            </Button>
        </form>
    );
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
