import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const isLoggedIn = localStorage.getItem('token') ? true : false;

  if (isLoggedIn) {
    return true;
  } else {
    router.navigateByUrl('/login');
    alert('Log in to access');
    return false;
  }
};
