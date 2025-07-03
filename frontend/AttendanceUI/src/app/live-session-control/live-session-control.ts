import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  LiveSessionModel,
  LiveSessionStudentsModel,
} from '../models/live-session-model';
import { LiveSessionService } from '../services/live-session-service';
import { AttendanceService } from '../services/attendance-service';
import { NotificationToast } from '../notification-toast/notification-toast';
import { NotificationService } from '../services/notification-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-live-session-control',
  imports: [FormsModule, NotificationToast],
  templateUrl: './live-session-control.html',
  styleUrl: './live-session-control.css',
})
export class LiveSessionControl {
  liveSession!: LiveSessionModel;
  liveSessionService = inject(LiveSessionService);
  attendanceService = inject(AttendanceService);
  notificationService = inject(NotificationService);
  router = inject(Router);
  isSessionClosed = false;
  isSessionCompleted = false;
  nameFilter = '';
  attendingExpanded = true;
  notJoinedExpanded = true;
  attendingStudents: LiveSessionStudentsModel[] = [];
  notJoinedStudents: LiveSessionStudentsModel[] = [];
  filteredAttending: LiveSessionStudentsModel[] = [];
  filteredNotJoined: LiveSessionStudentsModel[] = [];

  constructor() {
    this.liveSessionService.liveSessionDetails$.subscribe({
      next: (data) => {
        console.log(data);
        this.liveSession = data;
        this.attendingStudents = [...this.liveSession.attendingStudents];
        this.notJoinedStudents = [...this.liveSession.notJoinedStudents];
        this.filteredAttending = [...this.attendingStudents];
        this.filteredNotJoined = [...this.notJoinedStudents];
        console.log(this.notJoinedStudents);
      },
      error: (error) => console.log(error),
    });
    this.liveSessionService.updateLiveSession();
  }

  joinStudent(studentId: number) {
    const studentIndex = this.notJoinedStudents.findIndex(
      (s) => s.studentId === studentId
    );

    if (studentIndex >= 0) {
      this.attendanceService
        .markAttendance(studentId, this.liveSession.sessionId)
        .subscribe({
          next: (data) => {
            const [student] = this.notJoinedStudents.splice(studentIndex, 1);
            this.notificationService.addNotification({
              message: `Marked Attendance for ${student.studentName}`,
              type: 'success',
            });
            this.attendingStudents.push(student);
            this.applyNameFilter();
          },
          error: (error) => {
            this.notificationService.addNotification({
              message: `Unable to mark attendance`,
              type: 'danger',
            });
            console.log(error);
          },
        });
    }
  }

  unmarkStudent(studentId: number) {
    const studentIndex = this.attendingStudents.findIndex(
      (s) => s.studentId === studentId
    );
    if (studentIndex >= 0) {
      this.attendanceService
        .unmarkAttendance(studentId, this.liveSession.sessionId)
        .subscribe({
          next: (data) => {
            const [student] = this.attendingStudents.splice(studentIndex, 1);
            this.notificationService.addNotification({
              message: `Unmarked Attendance for ${student.studentName}`,
              type: 'success',
            });
            this.notJoinedStudents.push(student);
            this.applyNameFilter();
          },
          error: (error) => {
            this.notificationService.addNotification({
              message: `Unable to unmark attendance`,
              type: 'danger',
            });
            console.log(error);
          },
        });
    }
  }

  closeSession() {
    this.isSessionClosed = true;
  }

  completeSession() {
    this.liveSessionService
      .completeLiveSession(this.liveSession.sessionId)
      .subscribe({
        next: (data) => {
          this.isSessionCompleted = true;
          this.notificationService.addNotification({
            message: 'Session Completed Successfully',
            type: 'success',
          });
          this.router.navigateByUrl('/portal/dashboard');
        },
        error: (error) => {
          this.notificationService.addNotification({
            message: 'Failed to Completed Session',
            type: 'danger',
          });
          console.log(error);
        },
      });
  }

  applyNameFilter() {
    const keyword = this.nameFilter.trim().toLowerCase();
    this.filteredAttending = this.attendingStudents.filter((s) =>
      s.studentName.toLowerCase().includes(keyword)
    );
    this.filteredNotJoined = this.notJoinedStudents.filter((s) =>
      s.studentName.toLowerCase().includes(keyword)
    );
  }

  resetFilter() {
    this.nameFilter = '';
    this.filteredAttending = [...this.attendingStudents];
    this.filteredNotJoined = [...this.notJoinedStudents];
  }
}
