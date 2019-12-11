import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {AuctionsRoutingModule} from './auctions-routing.module';
import {StoreModule} from "@ngrx/store";
import {auctionsFeatureReducer} from "./reducers";
import {AUCTIONS_COMPONENTS} from "./components";
import {EffectsModule} from "@ngrx/effects";
import {AUCTIONS_EFFECTS} from "./effects";


@NgModule({
  declarations: [
    ...AUCTIONS_COMPONENTS
  ],
  imports: [
    CommonModule,
    AuctionsRoutingModule,
    StoreModule.forFeature('auctions', auctionsFeatureReducer),
    EffectsModule.forFeature([
      ...AUCTIONS_EFFECTS
    ])
  ]
})
export class AuctionsModule {
}
