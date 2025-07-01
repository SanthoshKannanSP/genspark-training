import { CanActivateFn, Router } from '@angular/router';
import { HttpClientService } from './services/http-client-service';
import { inject } from '@angular/core';

export const anyAuthGuard: CanActivateFn = (route, state) => {
  const api = inject(HttpClientService);
  const router = inject(Router);

  if (api.isAuthenticated()) {
    return true;
  }

  router.navigate(['/login']);
  return false;
};
