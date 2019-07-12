import * as React from 'react';
import { FormEvent, useState } from 'react';
import { Button, makeStyles, Typography } from '@material-ui/core';
import { Action, Dispatch } from 'redux';
import { connect } from 'react-redux';

import { CreateAuctionDto } from '../../common/store/dtos/auction-dto';
import { ValidationResultDto } from '../../common/store/dtos/validation-result-dto';
import { AppState } from '../../app-state';
import { isCreatingAuctionSelector } from '../../common/store/reducers/loading-reducer';
import { auctionsValidationResultSelector } from '../../common/store/reducers/auctions-reducer';
import { createAuctionRequestAction } from '../../common/store/actions/auction-actions';
import { ValidationResult } from '../../common/components/ValidationResult';
import { InputField } from '../../common/components/InputField';

const useStyles = makeStyles(theme => ({
    container: {
        display: 'flex',
        flexWrap: 'wrap',
        margin: theme.spacing(1),
    },
    saveButton: {
        marginTop: theme.spacing(2),
        alignSelf: 'right',
    },
}));

interface Props {
    isCreating: boolean;
    validationResult: ValidationResultDto | null;
    onSave: (dto: CreateAuctionDto) => void;
}

export function CreateAuctionView({ onSave, isCreating, validationResult }: Props) {
    const [name, setName] = useState('');
    const [date, setDate] = useState('');
    const classes = useStyles();

    const handleSave = (evt: FormEvent) => {
        if (evt) {
            evt.preventDefault();
        }

        onSave({ name, auctionDate: date });
    };
    return (
        <form className={classes.container} data-testid="create-auction" onSubmit={handleSave}>
            <Typography variant={'h6'}>Create Auction</Typography>
            <ValidationResult validationResult={validationResult} />
            <InputField
                value={name}
                autoFocus
                label={'Name'}
                onChange={t => setName(t.target.value)}
                inputProps={{ 'data-testid': 'create-auction-name-input' }}
                fullWidth
            />
            <InputField
                value={date}
                label={'Auction Date'}
                onChange={t => setDate(t.target.value)}
                inputProps={{ 'data-testid': 'create-auction-date-input' }}
                fullWidth
            />
            <Button
                className={classes.saveButton}
                data-testid={'create-auction-save-button'}
                type={'submit'}
                onClick={handleSave}
                disabled={isCreating}
            >
                Save
            </Button>
        </form>
    );
}

function mapStateToProps(state: AppState) {
    return {
        isCreating: isCreatingAuctionSelector(state),
        validationResult: auctionsValidationResultSelector(state),
    };
}

function mapDispatchToProps(dispatch: Dispatch<Action>) {
    return {
        onSave: (dto: CreateAuctionDto) => dispatch(createAuctionRequestAction(dto)),
    };
}

export const CreateAuction = connect(
    mapStateToProps,
    mapDispatchToProps,
)(CreateAuctionView);
