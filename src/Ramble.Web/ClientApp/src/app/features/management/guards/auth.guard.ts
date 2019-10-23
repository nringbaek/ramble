import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SessionQuery } from '../state';
import { map } from 'rxjs/operators';
import { ManagementServicesModule } from '../management-services.module';

@Injectable({
  providedIn: ManagementServicesModule
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private sessionQuery: SessionQuery
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.sessionQuery.isLoggedIn$.pipe(
      map(result => {
        if (result) {
          return true;
        }

        window.location.href = '/idp/account/login?returnurl=' + state.url;
        return false;
      })
    );
  }
}
