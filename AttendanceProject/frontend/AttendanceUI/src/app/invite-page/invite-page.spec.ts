import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { InvitePage } from './invite-page';
import { ActivatedRoute, Router } from '@angular/router';
import { SessionService } from '../services/session-service';
import { NotificationService } from '../services/notification-service';
import { of, throwError } from 'rxjs';

describe('InvitePage', () => {
  let component: InvitePage;
  let fixture: ComponentFixture<InvitePage>;
  let mockSessionService: jasmine.SpyObj<SessionService>;
  let mockNotificationService: jasmine.SpyObj<NotificationService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    const sessionSpy = jasmine.createSpyObj('SessionService', [
      'addStudentToSession',
    ]);
    const notificationSpy = jasmine.createSpyObj('NotificationService', [
      'addNotification',
    ]);
    routerSpy = jasmine.createSpyObj('Router', ['navigateByUrl']);

    TestBed.configureTestingModule({
      imports: [InvitePage],
      providers: [
        { provide: SessionService, useValue: sessionSpy },
        { provide: NotificationService, useValue: notificationSpy },
        { provide: Router, useValue: routerSpy },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: { sessionCode: 'abc123' },
            },
          },
        },
      ],
    });

    mockSessionService = TestBed.inject(
      SessionService
    ) as jasmine.SpyObj<SessionService>;
    mockNotificationService = TestBed.inject(
      NotificationService
    ) as jasmine.SpyObj<NotificationService>;

    fixture = TestBed.createComponent(InvitePage);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    mockSessionService.addStudentToSession.and.returnValue(of({}));
    fixture.detectChanges();

    expect(component).toBeTruthy();
  });

  it('should add student and show success notification', () => {
    mockSessionService.addStudentToSession.and.returnValue(of({}));

    fixture.detectChanges();

    expect(mockSessionService.addStudentToSession).toHaveBeenCalledWith(
      'abc123'
    );

    expect(mockNotificationService.addNotification).toHaveBeenCalledWith({
      message: 'Added to Session Successfully',
      type: 'success',
    });

    expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/portal/dashboard');
  });

  it('should show error notification on failure', () => {
    mockSessionService.addStudentToSession.and.returnValue(
      throwError(() => new Error('Invalid session'))
    );

    fixture.detectChanges();

    expect(mockSessionService.addStudentToSession).toHaveBeenCalledWith(
      'abc123'
    );

    expect(mockNotificationService.addNotification).toHaveBeenCalledWith({
      message: 'Invalid Session Code',
      type: 'danger',
    });

    expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/portal/dashboard');
  });
});
