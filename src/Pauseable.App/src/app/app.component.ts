import { Component } from '@angular/core';
import { BehaviorSubject, NEVER, Observable } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor() {

    const obs$ = new Observable(subscriber => {

      var source = new EventSource(`https://localhost:5001/api/notification/queue`);

      source.onmessage = function (event) {
        subscriber.next(event)
      };

      source.onopen = function(event) {

      };

      source.onerror = function(event) {
      }

    });

    const pauseableStream = this.pause$.pipe(switchMap(pause => pause ? NEVER : obs$ ));

    pauseableStream
    .pipe(
      tap(x => console.log(x))
    )
    .subscribe();

  }

  public pause$: BehaviorSubject<boolean> = new BehaviorSubject(true);

  public pause() {
    this.pause$.next(!this.pause$.value);
  }

}


