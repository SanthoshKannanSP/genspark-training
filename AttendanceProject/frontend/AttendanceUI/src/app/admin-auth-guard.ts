import { CanActivateFn, Router } from '@angular/router';
import { HttpClientService } from './services/http-client-service';
import { inject } from '@angular/core';

export const adminAuthGuard: CanActivateFn = (route, state) => {
  const api = inject(HttpClientService);
  const router = inject(Router);

  if (api.isAuthenticated() && api.hasRole('Admin')) {
    return true;
  }

  router.navigate(['/unauthorized']);
  return false;
};
