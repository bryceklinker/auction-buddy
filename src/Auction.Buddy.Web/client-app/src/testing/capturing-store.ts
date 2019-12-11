import { Inject, Injectable } from '@angular/core';
import { Action, ActionsSubject, INITIAL_STATE, ReducerManager,  } from '@ngrx/store';
import { MockSelector, MockState, MockStore } from '@ngrx/store/testing';

@Injectable()
export class CapturingStore<TState> extends MockStore<TState> {
  private _actions: Array<Action> = [];

  constructor(state$: MockState<TState>,
              actionsObserver: ActionsSubject,
              reducerManager: ReducerManager,
              @Inject(INITIAL_STATE) initialState: TState) {
    super(state$, actionsObserver, reducerManager, initialState, []);
  }

  dispatch<V extends Action = Action>(action: V): void {
    this._actions.push(action);
    super.dispatch(action);
  }

  getActions(): Array<Action> {
    return this._actions;
  }
}
