import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveSessionControl } from './live-session-control';
import { LiveSessionModel } from '../models/live-session-model';
import { of } from 'rxjs';
import { LiveSessionService } from '../services/live-session-service';
import { AttendanceService } from '../services/attendance-service';
import { NotificationService } from '../services/notification-service';
import { Router } from '@angular/router';
import { Component, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-notification-toast',
  template: '',
})
class MockNotificationToast {}

describe('LiveSessionControl', () => {
  let component: LiveSessionControl;
  let fixture: ComponentFixture<LiveSessionControl>;

  const mockLiveSessionDetails: LiveSessionModel = {
    sessionId: 1,
    sessionName: 'Test',
    attendingStudents: [
      {
        studentId: 1,
        studentName: 'Student1',
      },
    ],
    notJoinedStudents: [
      {
        studentId: 2,
        studentName: 'Student2',
      },
    ],
  };

  beforeEach(async () => {
    const liveSessionSpy = jasmine.createSpyObj(
      'LiveSessionService',
      ['updateLiveSession', 'completeLiveSession'],
      {
        liveSessionDetails$: of(mockLiveSessionDetails),
      }
    );

    const attendanceSpy = jasmine.createSpyObj('AttendanceService', [
      'markAttendance',
      'unmarkAttendance',
    ]);

    const notificationSpy = jasmine.createSpyObj('NotificationService', [
      'addNotification',
    ]);

    await TestBed.configureTestingModule({
      imports: [LiveSessionControl],
      providers: [
        { provide: LiveSessionService, useValue: liveSessionSpy },
        { provide: AttendanceService, useValue: attendanceSpy },
        { provide: NotificationService, useValue: notificationSpy },
        { provide: Router },
      ],
    })
      .overrideComponent(LiveSessionControl, {
        set: { imports: [MockNotificationToast, FormsModule] },
      })
      .compileComponents();

    fixture = TestBed.createComponent(LiveSessionControl);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
