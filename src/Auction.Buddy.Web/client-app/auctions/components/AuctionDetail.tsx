import * as React from "react";

import {AuctionDto} from "../../common/store/dtos/auction-dto";

interface Props {
    auction: AuctionDto;
}

export function AuctionDetail({auction}: Props) {
    return (
        <div>
            <div data-testid="auction-name">{auction.name}</div>
            <div data-testid="auction-date">{auction.auctionDate}</div>
        </div>
    )
}