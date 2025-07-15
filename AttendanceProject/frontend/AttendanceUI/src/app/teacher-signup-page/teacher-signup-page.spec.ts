import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherSignupPage } from './teacher-signup-page';
import { HttpClientService } from '../services/http-client-service';
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('TeacherSignupPage', () => {
  let component: TeacherSignupPage;
  let fixture: ComponentFixture<TeacherSignupPage>;
  let mockHttpClientService: jasmine.SpyObj<HttpClientService>;
  let mockRouter: jasmine.SpyObj<Router>;

  const mockTeacherForm = {
    name: 'John Doe',
    email: 'johndoe@gmail.com',
    organization: 'abcdef',
    password: 'johndoe123',
    confirmPassword: 'johndoe123',
  };

  beforeEach(async () => {
    const apiSpy = jasmine.createSpyObj('HttpClientService', ['post']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

    await TestBed.configureTestingModule({
      imports: [TeacherSignupPage],
      providers: [
        { provide: HttpClientService, useValue: apiSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(TeacherSignupPage);
    component = fixture.componentInstance;

    mockHttpClientService = TestBed.inject(
      HttpClientService
    ) as jasmine.SpyObj<HttpClientService>;
    mockRouter = TestBed.inject(Router) as jasmine.SpyObj<Router>;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should create account successfully', () => {
    mockHttpClientService.post.and.returnValue(of({}));

    component.signupForm.patchValue(mockTeacherForm);
    component.onSubmit();

    expect(mockHttpClientService.post).toHaveBeenCalledWith(
      '/api/v1/Teacher',
      mockTeacherForm
    );

    expect(mockRouter.navigateByUrl).toHaveBeenCalledOnceWith('/login');
  });
});
