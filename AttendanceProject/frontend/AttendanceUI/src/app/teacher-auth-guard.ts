import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { HttpClientService } from './services/http-client-service';

export const teacherAuthGuard: CanActivateFn = (route, state) => {
  const api = inject(HttpClientService);
  const router = inject(Router);

  if (api.isAuthenticated() && api.hasRole('Teacher') || api.hasRole('Admin') ) {
    return true;
  }

  router.navigate(['/unauthorized']);
  return false;
};
