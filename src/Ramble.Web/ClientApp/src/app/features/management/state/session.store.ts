import { Injectable } from '@angular/core';
import { Store, StoreConfig } from '@datorama/akita';
import { ManagementServicesModule } from '../management-services.module';

export interface SessionState {
  username: string;
  token: string;
}

export function createInitialState(): Partial<SessionState> {
  return {};
}

@StoreConfig({ name: 'session', resettable: true })
@Injectable({ providedIn: ManagementServicesModule })
export class SessionStore extends Store<SessionState> {

  constructor() {
    super(createInitialState());
  }

}

