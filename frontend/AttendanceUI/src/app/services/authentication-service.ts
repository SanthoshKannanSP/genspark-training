import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client-service';
import { tap, catchError, throwError, of } from 'rxjs';
import { LoginModel } from '../models/login-model';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private api: HttpClientService) {
    this.api.token = localStorage.getItem('access_token');
    this.api.refreshToken = localStorage.getItem('refresh_token');
  }

  login(loginData: LoginModel) {
    return this.api.post('/api/v1/Authentication', loginData).pipe(
      tap((res: any) => {
        if (res.data.token && res.data.refreshToken) {
          this.api.token = res.data.accessToken;
          this.api.refreshToken = res.data.refreshToken;

          localStorage.setItem('access_token', res.data.token);
          localStorage.setItem('refresh_token', res.data.refreshToken);
        } else {
          console.log(res);
          throw new Error('Invalid login response');
        }
      }),
      catchError((err) => throwError(() => err))
    );
  }

  logout() {
    this.api.post('/auth/logout', {}, true).subscribe({
      next: () => {},
      error: () => {},
    });

    this.api.token = null;
    this.api.refreshToken = null;

    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
  }

  isAuthenticated(): boolean {
    return !!this.api.token;
  }
}
