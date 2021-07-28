import { Component } from '@angular/core';
import { BehaviorSubject, NEVER, Observable } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private _pause$: BehaviorSubject<boolean> = new BehaviorSubject(true);

  constructor() {
    const stream$ = this._connect()

    this._pause$.pipe(
      switchMap(pause => pause ? NEVER : stream$ ),
      tap(notification => console.log(notification))
      )
    .subscribe();

  }

  public start() {
    this._pause$.next(false);
  }

  public stop() {
    this._pause$.next(true);
  }

  private _connect() {
    return new Observable(subscriber => {
      new EventSource(`https://localhost:5001/api/notification/queue`)
      .onmessage = function (event) {
        subscriber.next(event)
      };
    });
  }

}


