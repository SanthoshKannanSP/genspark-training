import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UpcomingSessionCard } from './upcoming-session-card';
import { HttpClientService } from '../../services/http-client-service';
import { AttendanceService } from '../../services/attendance-service';
import { SessionService } from '../../services/session-service';
import { NotificationService } from '../../services/notification-service';
import { Router } from '@angular/router';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { FormatTimePipe } from '../../misc/format-time-pipe';
import { of, throwError } from 'rxjs';
import { TeacherDetails } from '../../models/session-model';
import { SettingsService } from '../../services/settings-service';
import { DatePipe } from '@angular/common';

describe('UpcomingSessionCardComponent', () => {
  let component: UpcomingSessionCard;
  let fixture: ComponentFixture<UpcomingSessionCard>;

  let mockHttpClientService: jasmine.SpyObj<HttpClientService>;
  let mockSessionService: jasmine.SpyObj<SessionService>;
  let mockNotificationService: jasmine.SpyObj<NotificationService>;
  let mockRouter: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    const httpClientSpy = jasmine.createSpyObj('HttpClientService', [
      'hasRole',
    ]);

    const sessionSpy = {
      makeSessionLive: jasmine
        .createSpy('makeSessionLive')
        .and.returnValue(of({})),
    };
    const notificationSpy = {
      addNotification: jasmine.createSpy('addNotification').and.returnValue({}),
    };
    const routerSpy = {
      navigateByUrl: jasmine.createSpy('navigateByUrl').and.returnValue({}),
    };
    const settingsSpy = {
      getTimeFormat: jasmine
        .createSpy('getTimeFormat')
        .and.returnValue('hh:mm aa'),
      getDateFormat: jasmine
        .createSpy('getDateFormat')
        .and.returnValue('dd/MM/yyyy'),
    };

    await TestBed.configureTestingModule({
      imports: [UpcomingSessionCard, FormatDatePipe, FormatTimePipe],
      providers: [
        { provide: DatePipe },
        { provide: HttpClientService, useValue: httpClientSpy },
        { provide: SessionService, useValue: sessionSpy },
        { provide: NotificationService, useValue: notificationSpy },
        { provide: Router, useValue: routerSpy },
        { provide: SettingsService, useValue: settingsSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(UpcomingSessionCard);
    component = fixture.componentInstance;

    mockHttpClientService = TestBed.inject(
      HttpClientService
    ) as jasmine.SpyObj<HttpClientService>;
    mockSessionService = TestBed.inject(
      SessionService
    ) as jasmine.SpyObj<SessionService>;
    mockNotificationService = TestBed.inject(
      NotificationService
    ) as jasmine.SpyObj<NotificationService>;
    mockRouter = TestBed.inject(Router) as jasmine.SpyObj<Router>;

    component.session = {
      sessionId: 1,
      sessionName: 'Test Session',
      date: '2023-10-10',
      startTime: '10:00',
      endTime: '12:00',
      status: 'Scheduled',
      sessionLink: 'abc',
      sessionCode: 'avc',
      teacherDetails: new TeacherDetails(),
      attended: true,
    };

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should emit viewSession event when showDetails is called', () => {
    spyOn(component.viewSession, 'emit');
    component.showDetails();
    expect(component.viewSession.emit).toHaveBeenCalledWith(component.session);
  });

  it('should return true for isSessionLive if current time is within session time', () => {
    spyOn(component, 'isSessionLive').and.callThrough();
    const now = new Date();
    component.session.date = now.toISOString().split('T')[0];
    component.session.startTime = '00:00';
    component.session.endTime = '23:59';
    expect(component.isSessionLive()).toBeTrue();
  });

  it('should call makeSessionLive and navigate when joinLive is called by Teacher', () => {
    mockHttpClientService.hasRole.and.callFake((role) => role == 'Teacher');
    spyOn(window, 'open');
    component.joinLive();

    expect(mockSessionService.makeSessionLive).toHaveBeenCalledWith(
      component.session.sessionId
    );
    expect(mockNotificationService.addNotification).toHaveBeenCalledWith({
      message: 'Started Live Session. Joining...',
      type: 'success',
    });
    expect(mockRouter.navigateByUrl).toHaveBeenCalledWith('/session/live');
  });

  it('should open session link when joinLive is called by Student and session is Live', () => {
    mockHttpClientService.hasRole.and.callFake((role) => role == 'Student');
    component.session.status = 'Live';

    spyOn(window, 'open');

    component.joinLive();

    expect(window.open).toHaveBeenCalledWith(
      component.session.sessionLink,
      '_blank'
    );
  });

  it('should show notification if session cannot be joined', () => {
    mockHttpClientService.hasRole.and.returnValue(true);
    mockSessionService.makeSessionLive.and.returnValue(
      throwError(() => new Error('Unable to start session'))
    );

    component.joinLive();

    expect(mockNotificationService.addNotification).toHaveBeenCalledWith({
      message: 'Unable to start session',
      type: 'danger',
    });
  });
});
