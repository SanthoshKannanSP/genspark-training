import { TestBed } from '@angular/core/testing';
import { AccountService } from './account-service';
import { HttpClientService } from './http-client-service';
import { NotificationService } from './notification-service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { StudentModel } from '../models/student-model';
import { TeacherModel } from '../models/teacher-model';

describe('AccountService', () => {
  let service: AccountService;
  let httpClientSpy: jasmine.SpyObj<HttpClientService>;
  let notificationServiceSpy: jasmine.SpyObj<NotificationService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    const httpSpy = jasmine.createSpyObj('HttpClientService', [
      'get',
      'post',
      'delete',
      'hasRole',
    ]);
    const notificationSpy = jasmine.createSpyObj('NotificationService', [
      'addNotification',
    ]);
    const routeSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

    TestBed.configureTestingModule({
      providers: [
        AccountService,
        { provide: HttpClientService, useValue: httpSpy },
        { provide: NotificationService, useValue: notificationSpy },
        { provide: Router, useValue: routeSpy },
      ],
    });

    service = TestBed.inject(AccountService);
    httpClientSpy = TestBed.inject(
      HttpClientService
    ) as jasmine.SpyObj<HttpClientService>;
    notificationServiceSpy = TestBed.inject(
      NotificationService
    ) as jasmine.SpyObj<NotificationService>;
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  describe('getAccountDetails', () => {
    it('should get student details', () => {
      const mockStudent: StudentModel = {
        name: 'John Doe',
        dateOfBirth: '2003-02-01',
        gender: 'Male',
      };
      httpClientSpy.hasRole.and.callFake((role) => role == 'Student');
      httpClientSpy.get.and.returnValue(of({ data: mockStudent }));

      service.getAccountDetails();

      service.account$.subscribe((account) => {
        expect(account).toEqual(mockStudent);
      });

      expect(httpClientSpy.get).toHaveBeenCalledWith(
        '/api/v1/Student/Me',
        true
      );
    });

    it('should get teacher account details', () => {
      const mockTeacher: TeacherModel = {
        name: 'John Doe',
        organization: 'ABCXYZ',
      };
      httpClientSpy.hasRole.and.callFake((role) => role == 'Teacher');
      httpClientSpy.get.and.returnValue(of({ data: mockTeacher }));

      service.getAccountDetails();

      service.account$.subscribe((account) => {
        expect(account).toEqual(mockTeacher);
      });

      expect(httpClientSpy.get).toHaveBeenCalledWith(
        '/api/v1/Teacher/Me',
        true
      );
    });
  });

  describe('updateAccountDetails', () => {
    it('should update student details and show success notification', () => {
      const student: StudentModel = {
        name: 'John Doe',
        dateOfBirth: '2003-02-01',
        gender: 'Male',
      };
      httpClientSpy.hasRole.and.callFake((role) => role == 'Student');
      httpClientSpy.post.and.returnValue(of({ data: student }));

      service.updateAccountDetails(student);

      expect(httpClientSpy.post).toHaveBeenCalledWith(
        '/api/v1/Student/Update',
        student,
        true
      );
      expect(notificationServiceSpy.addNotification).toHaveBeenCalledWith({
        message: 'Successfully updated details',
        type: 'success',
      });
    });

    it('should show error notification if student update fails', () => {
      const student: StudentModel = {
        name: 'John Doe',
        dateOfBirth: '2003-02-01',
        gender: 'Male',
      };
      httpClientSpy.hasRole.and.callFake((role) => role == 'Student');
      httpClientSpy.post.and.returnValue(throwError(() => new Error('Error')));

      service.updateAccountDetails(student);

      expect(notificationServiceSpy.addNotification).toHaveBeenCalledWith({
        message: 'Failed to update details',
        type: 'danger',
      });
    });

    it('should update teacher details and show success notification', () => {
      const teacher: TeacherModel = {
        name: 'John Doe',
        organization: 'ABCXYZ',
      };
      httpClientSpy.hasRole.and.callFake((role) => role == 'Teacher');
      httpClientSpy.post.and.returnValue(of({ data: teacher }));

      service.updateAccountDetails(teacher);

      expect(httpClientSpy.post).toHaveBeenCalledWith(
        '/api/v1/Teacher/Update',
        teacher,
        true
      );
      expect(notificationServiceSpy.addNotification).toHaveBeenCalledWith({
        message: 'Successfully updated details',
        type: 'success',
      });
    });
  });

  describe('deleteAccount', () => {
    beforeEach(() => {
      spyOn(window, 'alert');
    });

    it('should delete student account and navigate to home', () => {
      httpClientSpy.hasRole.and.callFake((role) => role == 'Student');
      httpClientSpy.delete.and.returnValue(of({}));

      service.deleteAccount();

      expect(httpClientSpy.delete).toHaveBeenCalledWith(
        '/api/v1/Student',
        true
      );
      expect(window.alert).toHaveBeenCalledWith(
        'Successfully deleted the account'
      );
      expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/');
    });

    it('should delete teacher account and navigate to home', () => {
      httpClientSpy.hasRole.and.callFake((role) => role == 'Teacher');
      httpClientSpy.delete.and.returnValue(of({}));

      service.deleteAccount();

      expect(httpClientSpy.delete).toHaveBeenCalledWith(
        '/api/v1/Teacher',
        true
      );
      expect(window.alert).toHaveBeenCalledWith(
        'Successfully deleted the account'
      );
      expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/');
    });

    it('should show error notification if delete fails', () => {
      httpClientSpy.hasRole.and.callFake((role) => role == 'Student');
      httpClientSpy.delete.and.returnValue(
        throwError(() => new Error('Delete error'))
      );

      service.deleteAccount();

      expect(notificationServiceSpy.addNotification).toHaveBeenCalledWith({
        message: 'Unable to delete account',
        type: 'danger',
      });
    });
  });
});
