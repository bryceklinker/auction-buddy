import * as React from 'react';
import { ListItem, ListItemText } from '@material-ui/core';

import { AuctionDto } from '../../common/store/dtos/auction-dto';
import { formatDate } from '../../common/services/date-formatter';

interface Props {
    auction: AuctionDto;
    onSelected: (auction: AuctionDto) => void;
}

export function AuctionListItem({ auction, onSelected }: Props) {
    return (
        <ListItem button data-testid={'auction-item'} onClick={() => onSelected(auction)}>
            <ListItemText primary={auction.name} secondary={formatDate(auction.auctionDate)} />
        </ListItem>
    );
}
