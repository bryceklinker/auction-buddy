import * as React from 'react';
import { makeStyles } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
    container: {
        width: '100%',
        padding: theme.spacing(1),
        marginRight: theme.spacing(2),
        marginLeft: theme.spacing(2),
        color: theme.palette.error.main,
    },
}));

interface Props {
    hasLoginFailed: boolean;
}

export function LoginErrorMessage({ hasLoginFailed }: Props) {
    if (!hasLoginFailed) {
        return null;
    }

    const classes = useStyles();
    return (
        <div className={classes.container} data-testid="login-error">
            Invalid username or password
        </div>
    );
}
