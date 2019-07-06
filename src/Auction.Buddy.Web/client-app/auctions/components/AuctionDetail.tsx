import * as React from "react";
import { format } from 'date-fns';

import {AuctionDto} from "../../common/store/dtos/auction-dto";

interface Props {
    auction?: AuctionDto;
}

export function AuctionDetail({auction}: Props) {
    const auctionDate = auction 
        ? format(auction.auctionDate, 'MM/DD/YYYY')
        : '';
    
    return (
        <div>
            <div data-testid="auction-name">{auction ? auction.name : null}</div>
            <div data-testid="auction-date">{auctionDate}</div>
        </div>
    )
}