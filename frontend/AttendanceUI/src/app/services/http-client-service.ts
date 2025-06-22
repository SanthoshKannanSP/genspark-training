import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, switchMap, throwError, of } from 'rxjs';
import { Environment } from '../../environment/environment';

@Injectable()
export class HttpClientService {
  http = inject(HttpClient);

  token: string | null = null;
  refreshToken: string | null = null;

  private get headers(): HttpHeaders {
    return new HttpHeaders({
      Authorization: `Bearer ${this.token}`,
    });
  }

  get(route: string, credentials: boolean = false) {
    const url = Environment.BASE_URL + route;
    return this.http
      .get(url, { headers: credentials ? this.headers : undefined })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401 && credentials) {
            return this.handle401(() =>
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
          if (error.status === 401 && credentials) {
            return this.handle401(() =>
              this.http.post(url, body, { headers: this.headers })
            );
          }
          return throwError(() => error);
        })
      );
  }

  private handle401<T>(retryCallback: () => any) {
    if (!this.refreshToken) {
      return throwError(() => new Error('No refresh token available'));
    }

    return this.http
      .post<{ accessToken: string }>(
        '/auth/refresh',
        { refreshToken: this.refreshToken },
        { withCredentials: true }
      )
      .pipe(
        switchMap((res) => {
          if (!res.accessToken)
            return throwError(() => new Error('No new access token received'));
          this.token = res.accessToken;
          return retryCallback();
        }),
        catchError((err) => {
          return throwError(() => err);
        })
      );
  }
}
