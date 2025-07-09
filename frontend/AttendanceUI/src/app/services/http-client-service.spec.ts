import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { HttpClientService } from './http-client-service';
import { Environment } from '../../environment/environment';
import { LoginModel } from '../models/login-model';

describe('HttpClientService', () => {
  let api: HttpClientService;
  let httpMock: HttpTestingController;

  const mockAccessToken = 'mockAccessToken';
  const mockRefreshToken = 'mockRefreshToken';

  beforeEach(() => {
    localStorage.setItem('access_token', mockAccessToken);
    localStorage.setItem('refresh_token', mockRefreshToken);

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [HttpClientService],
    });

    api = TestBed.inject(HttpClientService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
    localStorage.clear();
  });

  it('should be created', () => {
    expect(api).toBeTruthy();
  });

  describe('get', () => {
    it('should make a GET request with credentials', () => {
      api.get('/test', true).subscribe();

      const req = httpMock.expectOne(Environment.BASE_URL + '/test');
      expect(req.request.method).toBe('GET');
      expect(req.request.headers.get('Authorization')).toBe(
        `Bearer ${mockAccessToken}`
      );
      req.flush({ data: 'ok' });
    });

    it('should retry GET request on 401 with refresh token', () => {
      const refreshedToken = 'newAccessToken';
      api.get('/test', true).subscribe();

      const req1 = httpMock.expectOne(Environment.BASE_URL + '/test');
      expect(req1.request.method).toBe('GET');
      req1.flush({}, { status: 401, statusText: 'Unauthorized' });

      const refreshReq = httpMock.expectOne(
        Environment.BASE_URL + '/api/v1/Authentication/Refresh'
      );
      expect(refreshReq.request.method).toBe('POST');
      refreshReq.flush({
        data: { accessToken: refreshedToken },
      });

      const retriedReq = httpMock.expectOne(Environment.BASE_URL + '/test');
      expect(retriedReq.request.headers.get('Authorization')).toBe(
        `Bearer ${refreshedToken}`
      );
      retriedReq.flush({ data: 'retry success' });
    });
  });

  describe('post', () => {
    it('should make a POST request with credentials', () => {
      api.post('/submit', { name: 'test' }, true).subscribe();

      const req = httpMock.expectOne(Environment.BASE_URL + '/submit');
      expect(req.request.method).toBe('POST');
      expect(req.request.headers.get('Authorization')).toBe(
        `Bearer ${mockAccessToken}`
      );
      req.flush({ success: true });
    });

    it('should retry POST request on 401 with refresh token', () => {
      const refreshedToken = 'newAccessToken';
      api.post('/submit', { name: 'test' }, true).subscribe();

      const req = httpMock.expectOne(Environment.BASE_URL + '/submit');
      req.flush({}, { status: 401, statusText: 'Unauthorized' });

      const refreshReq = httpMock.expectOne(
        Environment.BASE_URL + '/api/v1/Authentication/Refresh'
      );
      refreshReq.flush({
        data: { accessToken: refreshedToken },
      });

      const retriedReq = httpMock.expectOne(Environment.BASE_URL + '/submit');
      expect(retriedReq.request.headers.get('Authorization')).toBe(
        `Bearer ${refreshedToken}`
      );
      retriedReq.flush({ success: true });
    });
  });

  describe('delete', () => {
    it('should make a DELETE request with credentials', () => {
      api.delete('/remove', true).subscribe();

      const req = httpMock.expectOne(Environment.BASE_URL + '/remove');
      expect(req.request.method).toBe('DELETE');
      expect(req.request.headers.get('Authorization')).toBe(
        `Bearer ${mockAccessToken}`
      );
      req.flush({ deleted: true });
    });
  });

  describe('login', () => {
    it('should set access token and refresh tokens on login', () => {
      const loginData: LoginModel = {
        username: 'test@test.com',
        password: '1234',
      };

      api.login(loginData).subscribe();

      const req = httpMock.expectOne(
        Environment.BASE_URL + '/api/v1/Authentication'
      );
      req.flush({
        data: {
          token: 'newAccessToken',
          refreshToken: 'newRefreshToken',
        },
      });

      expect(localStorage.getItem('access_token')).toBe('newAccessToken');
      expect(localStorage.getItem('refresh_token')).toBe('newRefreshToken');
    });
  });

  describe('logout', () => {
    it('should clear access token and refresh token on logout', () => {
      api.logout().subscribe();

      const req = httpMock.expectOne(
        Environment.BASE_URL + '/api/v1/Authentication/Logout'
      );
      req.flush({ success: true });

      expect(localStorage.getItem('access_token')).toBeNull();
      expect(localStorage.getItem('refresh_token')).toBeNull();
    });
  });

  describe('isAuthenticated', () => {
    it('should return true if token exists', () => {
      expect(api.isAuthenticated()).toBeTrue();
    });

    it('should return false if no token', () => {
      localStorage.removeItem('access_token');
      api.token.next(null);
      expect(api.isAuthenticated()).toBeFalse();
    });
  });

  describe('hasRole', () => {
    it('should return true if token has correct role', () => {
      const encodedToken = btoa(
        JSON.stringify({
          exp: Math.floor(Date.now() / 1000) + 60,
          role: 'Student',
        })
      );
      const fakeToken = `header.${encodedToken}.signature`;
      api.token.next(fakeToken);

      expect(api.hasRole('Student')).toBeTrue();
    });

    it('should return false if token is missing or role does not match', () => {
      const encodedToken = btoa(
        JSON.stringify({
          exp: Math.floor(Date.now() / 1000) + 60,
          role: 'Student',
        })
      );
      const fakeToken = `header.${encodedToken}.signature`;
      api.token.next(fakeToken);
      expect(api.hasRole('Teacher')).toBeFalse();
    });
  });
});
