import * as React from 'react';
import { Route, RouteProps } from 'react-router';
import { connect } from 'react-redux';
import { AppState } from '../../app-state';
import { Dispatch } from 'redux';
import { push } from 'connected-react-router';
import { isUserLoggedInSelector } from '../../common/store/reducers/user-reducer';

interface Props extends RouteProps {
    isLoggedIn: boolean;
    onRedirectToLogin: () => void;
}

function LoginRequiredView({ component: Component, ...rest }: Props) {
    return (
        <Route
            {...rest}
            render={props => {
                if (rest.isLoggedIn) {
                    // @ts-ignore
                    return <Component {...props} />;
                }

                rest.onRedirectToLogin();
                return null;
            }}
        />
    );
}

function mapStateToProps(state: AppState) {
    return {
        isLoggedIn: isUserLoggedInSelector(state),
    };
}

function mapDispatchToProps(dispatch: Dispatch) {
    return {
        onRedirectToLogin: () => dispatch(push('/login')),
    };
}

export const LoginRequiredRoute = connect(
    mapStateToProps,
    mapDispatchToProps,
)(LoginRequiredView);
