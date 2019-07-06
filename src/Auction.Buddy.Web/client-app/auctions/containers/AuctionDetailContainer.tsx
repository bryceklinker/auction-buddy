import {connect} from "react-redux";
import {AuctionDetail} from "../components/AuctionDetail";
import {AppState} from "../../app-state";
import {auctionDetailSelector} from "../../common/store/reducers/auctions-reducer";
import {RouterProps} from "react-router";

function mapStateToProps(state: AppState, { match }: RouterProps) {
    return {
        auction: auctionDetailSelector(state, match.params['id'])
    };
}
export const AuctionDetailContainer = connect(mapStateToProps, null)(AuctionDetail);