import * as React from "react";
import {useState} from "react";
import {CreateAuctionDto} from "../../common/store/dtos/auction-dto";
import {ValidationResultDto} from "../../common/store/dtos/validation-result-dto";

interface Props {
    isCreating: boolean;
    validationResult: ValidationResultDto,
    onSave: (dto: CreateAuctionDto) => void;
}

export function CreateAuction({onSave, isCreating, validationResult}: Props) {
    const [name, setName] = useState('');
    const [date, setDate] = useState('');
    
    const handleSave = () => {
      onSave({ name, auctionDate: date});  
    };
    return (
        <div data-testid="create-auction">
            <ValidationResult validationResult={validationResult}/>
            <input data-testid="create-auction-name-input" value={name} onChange={t => setName(t.target.value)} />
            <input data-testid="create-auction-date-input" value={date} onChange={t => setDate(t.target.value)} />
            <button data-testid="create-auction-save-button" disabled={isCreating} onClick={handleSave} >Save</button>
        </div>
    )
}


interface ValidationResultProps {
    validationResult: ValidationResultDto;
}
export function ValidationResult({validationResult}: ValidationResultProps) {
    const shouldHide = !validationResult || validationResult.isValid;
    if (shouldHide) {
        return null;
    } 
    
    return (
        <div data-testid="validation-errors">
            <span>Auction is not valid</span>
        </div>
    ) 
}