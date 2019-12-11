import {auctionsReducer, AuctionsState} from "./auctions.reducer";
import {auctionItemsReducer, AuctionItemsState} from "./auction-items.reducer";
import {ActionReducerMap} from "@ngrx/store";

export interface AuctionsFeatureState {
  auctions: AuctionsState;
  auctionItems: AuctionItemsState;
}

export const auctionsFeatureReducer: ActionReducerMap<AuctionsFeatureState> = {
  auctions: auctionsReducer,
  auctionItems: auctionItemsReducer
};
