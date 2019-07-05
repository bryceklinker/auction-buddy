import {action} from "typesafe-actions";

export const InitActionTypes = {
    INIT: '@@init'
};

export function initAction() {
    return action(InitActionTypes.INIT);
}