import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
  catchError,
  switchMap,
  throwError,
  of,
  BehaviorSubject,
  tap,
} from 'rxjs';
import { Environment } from '../../environment/environment';
import { LoginModel } from '../models/login-model';
import { jwtDecode } from 'jwt-decode';

@Injectable()
export class HttpClientService {
  http = inject(HttpClient);

  token = new BehaviorSubject(localStorage.getItem('access_token'));
  token$ = this.token.asObservable();
  refreshToken = new BehaviorSubject(localStorage.getItem('refresh_token'));

  private get headers(): HttpHeaders {
    return new HttpHeaders({
      Authorization: `Bearer ${this.token.value}`,
    });
  }

  setToken(token: string) {
    this.token.next(token);
  }

  get(
    route: string,
    credentials: boolean = false,
    options: any = {},
    responseType: string = 'json'
  ) {
    const url = Environment.BASE_URL + route;
    return this.http
      .get(url, {
        headers: credentials ? this.headers : undefined,
        responseType: responseType,
        ...options,
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          console.log(error);
          if (credentials) {
            return this.tryRefresh(() =>
              this.http.get(url, { headers: this.headers })
            );
          }
          return throwError(() => error);
        })
      );
  }

  post(route: string, body: any, credentials: boolean = false) {
    const url = Environment.BASE_URL + route;
    return this.http
      .post(url, body, { headers: credentials ? this.headers : undefined })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (credentials) {
            return this.tryRefresh(() =>
              this.http.post(url, body, { headers: this.headers })
            );
          }
          return throwError(() => error);
        })
      );
  }

  delete(route: string, credentials: boolean = false) {
    const url = Environment.BASE_URL + route;
    return this.http
      .delete(url, { headers: credentials ? this.headers : undefined })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (credentials) {
            return this.tryRefresh(() =>
              this.http.delete(url, { headers: this.headers })
            );
          }
          return throwError(() => error);
        })
      );
  }

  private tryRefresh(retryCallback: () => any) {
    if (!this.refreshToken) {
      return throwError(() => new Error('No refresh token available'));
    }

    console.log('Refreshing token');

    return this.http
      .post<any>(
        Environment.BASE_URL + '/api/v1/Authentication/Refresh',
        {
          accessToken: this.token.value,
          refreshToken: this.refreshToken.value,
        },
        { headers: this.headers }
      )
      .pipe(
        switchMap((res) => {
          if (!res.data.accessToken)
            return throwError(() => new Error('No new access token received'));
          this.token.next(res.data.accessToken);

          localStorage.setItem('access_token', res.data.accessToken);
          return retryCallback();
        }),
        catchError((err) => {
          return throwError(() => err);
        })
      );
  }

  login(loginData: LoginModel) {
    return this.http
      .post(Environment.BASE_URL + '/api/v1/Authentication', loginData)
      .pipe(
        tap((res: any) => {
          if (res.data.token && res.data.refreshToken) {
            this.token.next(res.data.token);
            this.refreshToken.next(res.data.refreshToken);

            localStorage.setItem('access_token', res.data.token);
            localStorage.setItem('refresh_token', res.data.refreshToken);
          } else {
            throw new Error('Invalid login response');
          }
        }),
        catchError((err) => throwError(() => err))
      );
  }

  logout() {
    return this.http
      .post(
        Environment.BASE_URL + '/api/v1/Authentication/Logout',
        {},
        { headers: this.headers }
      )
      .pipe(
        tap((res: any) => {
          if (res.success != null && res.success) {
            this.token.next(null);
            this.refreshToken.next(null);

            localStorage.removeItem('access_token');
            localStorage.removeItem('refresh_token');
          } else {
            throw new Error('Invalid logit response');
          }
        }),
        catchError((err) => throwError(() => err))
      );
  }

  isAuthenticated(): boolean {
    return !!this.token.value;
  }

  hasRole(role: string) {
    if (this.token.value == null) return false;

    const decoded: any = jwtDecode(this.token.value);

    if (decoded?.role == role) return true;

    return false;
  }

  getUsername() {
    if (this.token.value == null) return '';
    const decoded: any = jwtDecode(this.token.value);

    return decoded.unique_name;
  }
}
