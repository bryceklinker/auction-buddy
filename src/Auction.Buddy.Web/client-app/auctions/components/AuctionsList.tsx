import * as React from 'react';
import { AuctionDto } from '../../common/store/dtos/auction-dto';
import { AppState } from '../../app-state';
import { auctionsSelector } from '../../common/store/reducers/auctions-reducer';
import { Dispatch } from 'redux';
import { push } from 'connected-react-router';
import { connect } from 'react-redux';
import { Fab, List, makeStyles } from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import { AuctionListItem } from './AuctionListItem';

const useStyles = makeStyles(theme => ({
    container: {
        display: 'flex',
        margin: theme.spacing(1),
        flexDirection: 'column',
    },
}));

interface Props {
    auctions: AuctionDto[];
    onCreate: () => void;
    onAuctionSelected: (auction: AuctionDto) => void;
}

function AuctionsListView({ auctions, onCreate, onAuctionSelected }: Props) {
    const classes = useStyles();
    const items = auctions.map(a => <AuctionListItem onSelected={onAuctionSelected} auction={a} key={a.id} />);
    return (
        <div className={classes.container} data-testid="auction-list">
            <List>{items}</List>
            <Fab data-testid={'create-auction-button'} onClick={onCreate} color={'primary'}>
                <AddIcon />
            </Fab>
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
        onAuctionSelected: (auction: AuctionDto) => dispatch(push(`/auction-detail/${auction.id}`)),
    };
}

export const AuctionsList = connect(
    mapStateToProps,
    mapDispatchToProps,
)(AuctionsListView);
