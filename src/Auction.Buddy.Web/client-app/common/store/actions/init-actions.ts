import {action, PayloadAction} from "typesafe-actions";

export const InitActionTypes = {
    INIT: '@@init'
};

export interface InitAction extends PayloadAction<string, null> {
    type: typeof InitActionTypes.INIT;
}

export function initAction(): InitAction {
    return action(InitActionTypes.INIT, null);
}

export type InitActions = InitAction;