import { makeStyles, TextField} from '@material-ui/core';
import { TextFieldProps } from '@material-ui/core/TextField';
import * as React from 'react';
import { ChangeEvent, useState } from 'react';

const useStyles = makeStyles(theme => ({
    textField: {
        marginLeft: theme.spacing(2),
        marginRight: theme.spacing(2),
        marginTop: theme.spacing(1),
    },
}));

export function InputField(props: TextFieldProps) {
    const classes = useStyles();
    const [value, setValue] = useState(props.value);
    const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
        if (props.onChange) {
            props.onChange(event);
        }
        setValue(event.target.value);
    };

    return (
        <TextField
            className={classes.textField}
            {...props}
            value={value}
            variant={'outlined'}
            onChange={handleChange}
        />
    );
}
