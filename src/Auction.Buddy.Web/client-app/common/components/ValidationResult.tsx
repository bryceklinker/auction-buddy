import { ValidationResultDto } from '../store/dtos/validation-result-dto';
import * as React from 'react';

interface ValidationResultProps {
    validationResult: ValidationResultDto | null;
}
export function ValidationResult({ validationResult }: ValidationResultProps) {
    const shouldHide = !validationResult || validationResult.isValid;
    if (shouldHide) {
        return null;
    }

    return (
        <div data-testid="validation-errors">
            <span>Auction is not valid</span>
        </div>
    );
}
