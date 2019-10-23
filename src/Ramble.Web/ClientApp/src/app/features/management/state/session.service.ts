import { Injectable } from '@angular/core';
import { SessionStore } from './session.store';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { ManagementServicesModule } from '../management-services.module';

@Injectable({ providedIn: ManagementServicesModule })
export class SessionService {

  constructor(
    private http: HttpClient,
    private sessionStore: SessionStore) {
  }

  get() {
    return this.http.get('').pipe(tap(entities => this.sessionStore.update(entities)));
  }

  signIn() {
    this.sessionStore.update({
      token: 'somevalue',
      username: 'nicolai'
    });
  }

  signOut() {
    this.sessionStore.reset();
  }
}
