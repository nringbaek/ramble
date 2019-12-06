import { Injectable } from '@angular/core';
import { SessionStore } from './session.store';
import { HttpClient } from '@angular/common/http';
import { tap, map, catchError } from 'rxjs/operators';
import { ManagementServicesModule } from '../management-services.module';
import { Observable, of } from 'rxjs';

@Injectable({ providedIn: ManagementServicesModule })
export class SessionService {

  constructor(
    private http: HttpClient,
    private sessionStore: SessionStore) {
  }

  refreshSession() {
    return this.http.get('api/v1/management/session')
      .pipe(
        tap(entities => this.sessionStore.update(entities))
      );
  }

  signIn(returnUrl: string = null): Observable<boolean> {
    return this.refreshSession().pipe(
      catchError(error => {
        if (returnUrl == null) {
          returnUrl = window.location.pathname + window.location.search;
        }

        window.location.href = '/_authentication/signin?returnurl=' + encodeURIComponent(returnUrl);
        return of(false);
      }),
      map(result => {
        if (result === false) {
          return false;
        }

        return true;
      })
    );
  }

  signOut() {
    this.sessionStore.reset();
  }
}
