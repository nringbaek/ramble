import { Injectable } from '@angular/core';
import { Store, StoreConfig } from '@datorama/akita';
import { ManagementServicesModule } from '../management-services.module';

export interface SessionState {
  username: string;
  bearerToken: string;
  showFrontpage: boolean;
  isNewSession: boolean;
}

export function createInitialState(): Partial<SessionState> {
  return {
    isNewSession: true
  };
}

@StoreConfig({ name: 'session', resettable: true })
@Injectable({ providedIn: ManagementServicesModule })
export class SessionStore extends Store<SessionState> {

  constructor() {
    super(createInitialState());
  }
}

