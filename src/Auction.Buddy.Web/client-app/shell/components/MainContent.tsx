import * as React from 'react';
import { makeStyles, Paper } from '@material-ui/core';

interface Props {
    children: JSX.Element | JSX.Element[];
}

const useStyles = makeStyles({
    main: {
        paddingTop: '96px',
        width: '100%',
        marginRight: '16px',
        marginLeft: '16px',
    },
});

export function MainContent({ children }: Props) {
    const classes = useStyles();
    return <Paper className={classes.main}>{children}</Paper>;
}
