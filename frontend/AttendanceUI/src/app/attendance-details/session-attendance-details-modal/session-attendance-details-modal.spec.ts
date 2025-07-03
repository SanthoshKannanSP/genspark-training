import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionAttendanceDetailsModal } from './session-attendance-details-modal';
import { ReactiveFormsModule } from '@angular/forms';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { AttendanceService } from '../../services/attendance-service';
import { NotificationService } from '../../services/notification-service';
import { PaginatedResponse } from '../../models/paginated-response';
import { SessionAttendanceModel } from '../../models/session-attendance-modal';
import { of } from 'rxjs';
import { DatePipe } from '@angular/common';
import { SettingsService } from '../../services/settings-service';
import { AttendanceDetailsModel } from '../../models/attendance-details-modal';

describe('SessionAttendanceDetailsModal', () => {
  let component: SessionAttendanceDetailsModal;
  let fixture: ComponentFixture<SessionAttendanceDetailsModal>;
  let attendanceService: jasmine.SpyObj<AttendanceService>;
  let notificationService: jasmine.SpyObj<NotificationService>;
  let settingsService: jasmine.SpyObj<SettingsService>;

  const mockStudents: PaginatedResponse<SessionAttendanceModel> = {
    data: {
      registeredCount: 2,
      attendedCount: 1,
      sessionAttendance: [
        {
          studentId: 1,
          studentName: 'John Doe',
          attended: true,
          sessionId: 101,
        },
        {
          studentId: 2,
          studentName: 'Jane Smith',
          attended: false,
          sessionId: 101,
        },
      ],
    },
    pagination: {
      page: 1,
      pageSize: 10,
      totalPages: 2,
      totalRecords: 20,
    },
  };

  beforeEach(async () => {
    const attendanceServiceSpy = jasmine.createSpyObj(
      'AttendanceService',
      ['getSessionAttendance', 'markAttendance', 'unmarkAttendance'],
      {
        sessionAttendanceDetails$: of(mockStudents),
      }
    );
    const notificationServiceSpy = jasmine.createSpyObj('NotificationService', [
      'addNotification',
    ]);
    const settingsServiceSpy = {
      getDateFormat: jasmine
        .createSpy('getDateFormat')
        .and.returnValue('dd/MM/yyyy'),
    };

    await TestBed.configureTestingModule({
      imports: [
        SessionAttendanceDetailsModal,
        ReactiveFormsModule,
        FormatDatePipe,
      ],
      providers: [
        { provide: AttendanceService, useValue: attendanceServiceSpy },
        { provide: NotificationService, useValue: notificationServiceSpy },
        { provide: SettingsService, useValue: settingsServiceSpy },
        { provide: DatePipe },
      ],
    }).compileComponents();

    attendanceService = TestBed.inject(
      AttendanceService
    ) as jasmine.SpyObj<AttendanceService>;
    notificationService = TestBed.inject(
      NotificationService
    ) as jasmine.SpyObj<NotificationService>;
    settingsService = TestBed.inject(
      SettingsService
    ) as jasmine.SpyObj<SettingsService>;

    fixture = TestBed.createComponent(SessionAttendanceDetailsModal);
    component = fixture.componentInstance;
    component.session = {
      sessionName: 'Math Class',
      date: '2025-08-09',
      startTime: '09:00:00',
      endTime: '11:00:00',
      attendedCount: 1,
      registeredCount: 2,
      sessionId: 101,
      status: 'Scheduled',
    };

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display session details', () => {
    const sessionNameElement =
      fixture.nativeElement.querySelector('.sessionName');
    const sessionDateElement =
      fixture.nativeElement.querySelector('.sessionDate');
    const sessionTimeElement =
      fixture.nativeElement.querySelector('.sessionTime');
    const sessionRegisteredElement =
      fixture.nativeElement.querySelector('.sessionRegistered');
    const sessionAttendedElement =
      fixture.nativeElement.querySelector('.sessionAttended');
    expect(sessionNameElement.textContent).toContain('Session: Math Class');
    expect(sessionDateElement.textContent).toContain('Date: 09/08/2025');
    expect(sessionTimeElement.textContent).toContain(
      'Time: 09:00:00 - 11:00:00'
    );
    expect(sessionRegisteredElement.textContent).toContain('Registered: 2');
    expect(sessionAttendedElement.textContent).toContain('Attended: 1');
  });

  it('should apply filter', () => {
    component.filterForm.setValue({ studentName: 'John', attended: true });

    component.applyFilter();

    expect(attendanceService.getSessionAttendance).toHaveBeenCalledWith(
      101,
      null,
      null,
      { studentName: 'John', attended: true }
    );
  });

  it('should mark attendance on toggle', () => {
    const student = mockStudents.data!.sessionAttendance[1];
    const fakeEvent = { target: { checked: true } } as unknown as Event;

    attendanceService.markAttendance.and.returnValue(of({}));
    component.session = {
      sessionId: 101,
      sessionName: 'Test Session',
    } as AttendanceDetailsModel;

    component.toggleAttendance(fakeEvent, student, 2);

    expect(attendanceService.markAttendance).toHaveBeenCalledWith(2, 101);
  });
});
