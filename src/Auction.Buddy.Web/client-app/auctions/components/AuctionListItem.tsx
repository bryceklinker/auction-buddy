import * as React from 'react';
import { ListItem, ListItemText } from '@material-ui/core';

import { AuctionDto } from '../../common/store/dtos/auction-dto';
import { formatDate } from '../../common/services/date-formatter';

interface Props {
    auction: AuctionDto;
}

export function AuctionListItem({ auction }: Props) {
    return (
        <ListItem data-testid={'auction-item'}>
            <ListItemText primary={auction.name} secondary={formatDate(auction.auctionDate)} />
        </ListItem>
    );
}
