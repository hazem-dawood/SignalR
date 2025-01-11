import { HttpInterceptorFn } from '@angular/common/http';
import { HttpRequest, HttpHandlerFn, HttpEvent, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';

import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { AuthenticateService } from '../services/auth/authenticate.service';

export const setTokenInterceptor: HttpInterceptorFn = (request: HttpRequest<unknown>, next: HttpHandlerFn):
  Observable<HttpEvent<unknown>> => {
  // Injecting dependencies using the inject() function
  const authenticateService = inject(AuthenticateService);
  const router = inject(Router);

  // Get the token if the user is authenticated
  let token = '';
  if (authenticateService.isUserAuthenticated()) {
    token = authenticateService.getToken();
  }


  if (token) {
    request = request.clone({
      setHeaders: {
        'authorization': 'Bearer ' + token,
      }
    });
  } else {
    request = request.clone({
      setHeaders: {}
    });
  }

  // Handle the response
  return next(request).pipe(
    tap(
      (event) => {
        if (event instanceof HttpResponse) {
          const to = event.headers.get('token');
          authenticateService.setLastAntToken(to);
        }
      },
      (error) => {
        if (error.status === 401) {
          authenticateService.removeAuthentication();
          router.navigate(['/login']);
        }
      }
    )
  );
};
