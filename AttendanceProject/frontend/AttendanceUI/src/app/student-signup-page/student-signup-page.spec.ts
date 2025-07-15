import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentSignupPage } from './student-signup-page';
import { HttpClientService } from '../services/http-client-service';
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('StudentSignupPage', () => {
  let component: StudentSignupPage;
  let fixture: ComponentFixture<StudentSignupPage>;
  let mockHttpClientService: jasmine.SpyObj<HttpClientService>;
  let mockRouter: jasmine.SpyObj<Router>;

  const mockStudentForm = {
    name: 'John Doe',
    gender: 'Male',
    dateOfBirth: '2003-01-02',
    email: 'johndoe@gmail.com',
    password: 'johndoe123',
    confirmPassword: 'johndoe123',
  };

  beforeEach(async () => {
    const apiSpy = jasmine.createSpyObj('HttpClientService', ['post']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

    await TestBed.configureTestingModule({
      imports: [StudentSignupPage],
      providers: [
        { provide: HttpClientService, useValue: apiSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(StudentSignupPage);
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

    component.signupForm.patchValue(mockStudentForm);
    component.onSubmit();

    expect(mockHttpClientService.post).toHaveBeenCalledWith(
      '/api/v1/Student',
      mockStudentForm
    );

    expect(mockRouter.navigateByUrl).toHaveBeenCalledOnceWith('/login');
  });
});
