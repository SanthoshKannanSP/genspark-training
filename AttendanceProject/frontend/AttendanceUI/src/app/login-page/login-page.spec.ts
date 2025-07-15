import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginPage } from './login-page';
import { HttpClientService } from '../services/http-client-service';
import { Router } from '@angular/router';
import { LoginModel } from '../models/login-model';
import { of, throwError } from 'rxjs';

describe('LoginPage', () => {
  let component: LoginPage;
  let fixture: ComponentFixture<LoginPage>;
  let mockHttpClientService: jasmine.SpyObj<HttpClientService>;
  let mockRouterService: jasmine.SpyObj<Router>;

  const mockCredentials: LoginModel = {
    username: 'johndoe@gmail.com',
    password: 'johndoe123',
  };

  beforeEach(async () => {
    const apiSpy = jasmine.createSpyObj('HttpClientService', ['login']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);
    await TestBed.configureTestingModule({
      imports: [LoginPage],
      providers: [
        { provide: HttpClientService, useValue: apiSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginPage);
    component = fixture.componentInstance;

    mockHttpClientService = TestBed.inject(
      HttpClientService
    ) as jasmine.SpyObj<HttpClientService>;
    mockRouterService = TestBed.inject(Router) as jasmine.SpyObj<Router>;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should login successfully if credentials are correct', () => {
    mockHttpClientService.login.and.returnValue(of({}));
    spyOn(window, 'alert');

    component.loginForm.patchValue(mockCredentials);
    component.onSubmit();

    expect(mockHttpClientService.login).toHaveBeenCalledWith(mockCredentials);
    expect(mockRouterService.navigateByUrl).toHaveBeenCalledOnceWith(
      'portal/dashboard'
    );
    expect(window.alert).toHaveBeenCalledWith(
      'Successfully logged in. Redirecting...'
    );
  });

  it('should alert if credentials are wrong', () => {
    mockHttpClientService.login.and.returnValue(
      throwError(() => new Error('Invalid credentials'))
    );
    spyOn(window, 'alert');

    component.loginForm.patchValue(mockCredentials);
    component.onSubmit();

    expect(mockHttpClientService.login).toHaveBeenCalledWith(mockCredentials);
    expect(window.alert).toHaveBeenCalledWith(
      'Error: Invalid email or password'
    );
  });
});
