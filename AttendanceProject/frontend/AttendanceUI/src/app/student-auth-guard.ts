import { CanActivateFn, Router } from '@angular/router';
import { HttpClientService } from './services/http-client-service';
import { inject } from '@angular/core';

export const studentAuthGuard: CanActivateFn = (route, state) => {
  const api = inject(HttpClientService);
  const router = inject(Router);

  if (api.isAuthenticated() && api.hasRole('Student')) {
    return true;
  }

  router.navigate(['/unauthorized']);
  return false;
};
