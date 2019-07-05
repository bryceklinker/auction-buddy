import * as React from "react";
import {useState} from "react";
import {CreateAuctionDto} from "../../common/store/dtos/auction-dto";

interface Props {
    onSave: (dto: CreateAuctionDto) => void;
}

export function CreateAuction({onSave}: Props) {
    const [name, setName] = useState('');
    const [date, setDate] = useState('');
    
    const handleSave = () => {
      onSave({ name, auctionDate: date});  
    };
    return (
        <div data-testid="create-auction">
            <input data-testid="create-auction-name-input" value={name} onChange={t => setName(t.target.value)} />
            <input data-testid="create-auction-date-input" value={date} onChange={t => setDate(t.target.value)} />
            <button data-testid="create-auction-save-button" onClick={handleSave} />
        </div>
    )
}