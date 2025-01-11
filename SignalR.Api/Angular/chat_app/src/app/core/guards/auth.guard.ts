import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthenticateService } from '../services/auth/authenticate.service';

export const authGuard: CanActivateFn = (route, state) => {
  var authenticateService = inject(AuthenticateService);
  var isUserAuth = authenticateService.isUserAuthenticated();
  if (isUserAuth) {
    return true;
  }
  var router = inject(Router);
  router.navigate(['/login'])
  return false;
};
