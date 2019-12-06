import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { SessionQuery, SessionService } from '../state';
import { map, switchMap } from 'rxjs/operators';
import { ManagementServicesModule } from '../management-services.module';

@Injectable({
  providedIn: ManagementServicesModule
})
export class AuthGuard implements CanActivate {
  constructor(
    private sessionQuery: SessionQuery,
    private sessionService: SessionService
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.sessionQuery.isLoggedIn$.pipe(
      switchMap(result => {
        if (result) {
          return of(true);
        }

        return this.sessionQuery.isNewSession$.pipe(
          switchMap(isNewSession => {
            if (isNewSession) {
              return this.sessionService.signIn(state.url);
            } else {
              return of(false);
            }
          })
        );
      })
    );
  }
}
