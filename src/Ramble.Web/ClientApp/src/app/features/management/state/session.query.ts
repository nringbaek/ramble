import { Injectable } from '@angular/core';
import { Query } from '@datorama/akita';
import { SessionStore, SessionState } from './session.store';
import { ManagementServicesModule } from '../management-services.module';

@Injectable({ providedIn: ManagementServicesModule })
export class SessionQuery extends Query<SessionState> {
  isLoggedIn$ = this.select(s => Boolean(s.bearerToken));
  isNewSession$ = this.select(s => s.isNewSession);

  constructor(protected store: SessionStore) {
    super(store);
  }
}
