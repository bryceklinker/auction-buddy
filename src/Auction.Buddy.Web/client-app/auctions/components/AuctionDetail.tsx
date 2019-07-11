import * as React from 'react';
import { format } from 'date-fns';

import { AuctionDto } from '../../common/store/dtos/auction-dto';
import { AppState } from '../../app-state';
import { RouteComponentProps } from 'react-router';
import { auctionDetailSelector } from '../../common/store/reducers/auctions-reducer';
import { connect } from 'react-redux';

interface Props {
    auction?: AuctionDto;
}

function AuctionDetailView({ auction }: Props) {
    const auctionDate = auction ? format(auction.auctionDate, 'MM/DD/YYYY') : '';

    return (
        <div>
            <div data-testid="auction-name">{auction ? auction.name : null}</div>
            <div data-testid="auction-date">{auctionDate}</div>
        </div>
    );
}

function mapStateToProps(state: AppState, { match }: RouteComponentProps<{ id: string }>) {
    return {
        auction: auctionDetailSelector(state, match.params.id),
    };
}
export const AuctionDetail = connect(
    mapStateToProps,
    null,
)(AuctionDetailView);
