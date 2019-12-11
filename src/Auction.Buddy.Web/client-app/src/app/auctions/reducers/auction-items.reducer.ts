import {ActionReducer} from "@ngrx/store";
import {EntityState} from "@ngrx/entity";
import {AuctionItemModel} from "../models/auction-item.model";

export interface AuctionItemsState extends EntityState<AuctionItemModel> {

}


export const auctionItemsReducer: ActionReducer<AuctionItemsState> = (state, action) => {
  return state;
};
