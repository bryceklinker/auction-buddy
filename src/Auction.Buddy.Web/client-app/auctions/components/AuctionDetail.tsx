import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';

import { AuctionDto } from '../../common/store/dtos/auction-dto';
import { AppState } from '../../app-state';
import { auctionDetailSelector } from '../../common/store/reducers/auctions-reducer';
import { formatDate } from '../../common/services/date-formatter';

interface Props {
    auction?: AuctionDto;
}

function AuctionDetailView({ auction }: Props) {
    const auctionDate = auction ? formatDate(auction.auctionDate) : '';

    return (
        <div data-testid="auction-details">
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
