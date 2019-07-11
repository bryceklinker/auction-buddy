import * as React from 'react';
import { useState } from 'react';
import { CreateAuctionDto } from '../../common/store/dtos/auction-dto';
import { ValidationResultDto } from '../../common/store/dtos/validation-result-dto';
import { AppState } from '../../app-state';
import { isCreatingAuctionSelector } from '../../common/store/reducers/loading-reducer';
import { auctionsValidationResultSelector } from '../../common/store/reducers/auctions-reducer';
import { Action, Dispatch } from 'redux';
import { createAuctionRequestAction } from '../../common/store/actions/auction-actions';
import { connect } from 'react-redux';
import { ValidationResult } from '../../common/components/ValidationResult';

interface Props {
    isCreating: boolean;
    validationResult: ValidationResultDto | null;
    onSave: (dto: CreateAuctionDto) => void;
}

export function CreateAuctionView({ onSave, isCreating, validationResult }: Props) {
    const [name, setName] = useState('');
    const [date, setDate] = useState('');

    const handleSave = () => {
        onSave({ name, auctionDate: date });
    };
    return (
        <div data-testid="create-auction">
            <ValidationResult validationResult={validationResult} />
            <input data-testid="create-auction-name-input" value={name} onChange={t => setName(t.target.value)} />
            <input data-testid="create-auction-date-input" value={date} onChange={t => setDate(t.target.value)} />
            <button data-testid="create-auction-save-button" disabled={isCreating} onClick={handleSave}>
                Save
            </button>
        </div>
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
