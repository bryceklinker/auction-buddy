import { runSaga, RunSagaOptions, Saga } from 'redux-saga';
import {Action} from "redux";

import {AppState} from "../app-state";

export async function recordSaga<TSaga extends Saga<Action[]>>(saga: TSaga, initialAction: Action): Promise<Action[]> {
    const dispatched: Action[] = [];
    
    const options: RunSagaOptions<Action, AppState> = {
        dispatch(output: Action): any {
            dispatched.push(output);
        }
    };
    // @ts-ignore
    await runSaga(options, saga, initialAction).toPromise();
    return dispatched;
}