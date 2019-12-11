import {Actions} from "@ngrx/effects";
import {Injectable} from "@angular/core";

@Injectable()
export class ShellEffects {
  constructor(private actions$: Actions) {}
}
