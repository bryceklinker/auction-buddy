import * as React from 'react';
import { AuctionDto } from '../../common/store/dtos/auction-dto';
import { AppState } from '../../app-state';
import { auctionsSelector } from '../../common/store/reducers/auctions-reducer';
import { Dispatch } from 'redux';
import { push } from 'connected-react-router';
import { connect } from 'react-redux';

interface Props {
    auctions: AuctionDto[];
    onCreate: () => void;
}

function AuctionsListView({ auctions, onCreate }: Props) {
    const items = auctions.map(a => <li data-testid="auction-item" key={a.id}></li>);
    return (
        <div data-testid="auction-list">
            <ul>{items}</ul>
            <button data-testid="create-auction-button" onClick={onCreate}>
                Create Auction
            </button>
        </div>
    );
}

function mapStateToProps(state: AppState) {
    return {
        auctions: auctionsSelector(state),
    };
}

function mapDispatchToProps(dispatch: Dispatch) {
    return {
        onCreate: () => dispatch(push('/create-auction')),
    };
}

export const AuctionsList = connect(
    mapStateToProps,
    mapDispatchToProps,
)(AuctionsListView);
