import {ActionReducer} from "@ngrx/store";
import {EntityState} from "@ngrx/entity";
import {AuctionModel} from "../models/auction.model";

export interface AuctionsState extends EntityState<AuctionModel> {

}

export const auctionsReducer: ActionReducer<AuctionsState> = (state, action) => {
  return state;
};
