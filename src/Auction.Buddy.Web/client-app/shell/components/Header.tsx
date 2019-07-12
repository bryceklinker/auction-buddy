import * as React from 'react';
import { AppBar, makeStyles, Toolbar, Typography } from '@material-ui/core';

const useStyles = makeStyles({
    toolbar: {
        alignItems: 'center',
    },
});

export function Header() {
    const classes = useStyles();
    return (
        <AppBar className={classes.toolbar}>
            <Toolbar>
                <Typography variant={'h4'}>Auction Buddy</Typography>
            </Toolbar>
        </AppBar>
    );
}
