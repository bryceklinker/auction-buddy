import {connect} from "react-redux";

import {CreateAuction} from "../components/CreateAuction";
import {Action, Dispatch} from "redux";
import {createAuctionRequestAction} from "../../common/store/actions/auction-actions";
import {CreateAuctionDto} from "../../common/store/dtos/auction-dto";

function mapDispatchToProps(dispatch: Dispatch<Action>) {
    return {
        onSave: (dto: CreateAuctionDto) => dispatch(createAuctionRequestAction(dto))
    }
}

export const CreateAuctionContainer = connect(null, mapDispatchToProps)(CreateAuction);