import { inject, Injectable } from '@angular/core';
import { HttpClientService } from './http-client-service';
import { StudentModel } from '../models/student-model';
import { TeacherModel } from '../models/teacher-model';
import { BehaviorSubject } from 'rxjs';
import { NotificationService } from './notification-service';
import { Router } from '@angular/router';

@Injectable()
export class AccountService {
  api = inject(HttpClientService);
  notificationService = inject(NotificationService);
  router = inject(Router);
  account = new BehaviorSubject<StudentModel | TeacherModel>(
    new StudentModel()
  );
  account$ = this.account.asObservable();

  getAccountDetails() {
    if (this.api.hasRole('Student')) {
      this.api.get('/api/v1/Student/Me', true).subscribe({
        next: (data: any) => this.account.next(data.data as StudentModel),
        error: (error) => console.log(error),
      });
    } else if (this.api.hasRole('Teacher')|| this.api.hasRole('Admin')) {
      this.api.get('/api/v1/Teacher/Me', true).subscribe({
        next: (data: any) => this.account.next(data.data as TeacherModel),
        error: (error) => console.log(error),
      });
    }
  }

  updateAccountDetails(details: TeacherModel | StudentModel) {
    if (this.api.hasRole('Student')) {
      this.api.post('/api/v1/Student/Update', details, true).subscribe({
        next: (data: any) => {
          this.account.next(data.data as StudentModel);
          this.notificationService.addNotification({
            message: 'Successfully updated details',
            type: 'success',
          });
        },
        error: (error) => {
          console.log(error);
          this.notificationService.addNotification({
            message: 'Failed to update details',
            type: 'danger',
          });
        },
      });
    } else if (this.api.hasRole('Teacher') || this.api.hasRole('Admin') ) {
      this.api.post('/api/v1/Teacher/Update', details, true).subscribe({
        next: (data: any) => {
          this.account.next(data.data as TeacherModel);
          this.notificationService.addNotification({
            message: 'Successfully updated details',
            type: 'success',
          });
        },
        error: (error) => {
          console.log(error);
          this.notificationService.addNotification({
            message: 'Failed to update details',
            type: 'danger',
          });
        },
      });
    }
  }

  deleteAccount() {
    if (this.api.hasRole('Student')) {
      this.api.delete('/api/v1/Student', true).subscribe({
        next: (data) => {
          alert('Successfully deleted the account');
          this.router.navigateByUrl('/');
        },
        error: (error) => {
          console.log(error);
          this.notificationService.addNotification({
            message: 'Unable to delete account',
            type: 'danger',
          });
        },
      });
    } else if (this.api.hasRole('Teacher')|| this.api.hasRole('Admin')) {
      this.api.delete('/api/v1/Teacher', true).subscribe({
        next: (data) => {
          alert('Successfully deleted the account');
          this.router.navigateByUrl('/');
        },
        error: (error) => {
          console.log(error);
          this.notificationService.addNotification({
            message: 'Unable to delete account',
            type: 'danger',
          });
        },
      });
    }
  }
}
