import * as React from "react";
import {History} from "history";

interface Props {
    history: History;
}

export function AuctionsList({history}: Props) {
    const handleCreateAuction = () => history.push('/create-auction');
    return (
        <div data-testid="auction-list">
            <button data-testid="create-auction-button" onClick={handleCreateAuction}>Create Auction</button>
        </div>
    );
}