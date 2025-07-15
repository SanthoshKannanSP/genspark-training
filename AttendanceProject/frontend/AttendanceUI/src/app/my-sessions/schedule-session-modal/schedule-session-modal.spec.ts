import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleSessionModal } from './schedule-session-modal';
import { SessionService } from '../../services/session-service';
import { NotificationService } from '../../services/notification-service';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';

describe('ScheduleSessionModal', () => {
  let component: ScheduleSessionModal;
  let fixture: ComponentFixture<ScheduleSessionModal>;
  let mockSessionService: jasmine.SpyObj<SessionService>;
  let mockNotificationService: jasmine.SpyObj<NotificationService>;

  const mockScheduledSession = {
    data: {
      sessionName: 'Test',
    },
  };

  beforeEach(async () => {
    const sessionSpy = jasmine.createSpyObj('SessionService', [
      'scheduleSession',
    ]);
    const notificationSpy = jasmine.createSpyObj('notificationService', [
      'addNotification',
    ]);

    await TestBed.configureTestingModule({
      imports: [ScheduleSessionModal],
      providers: [
        { provide: SessionService, useValue: sessionSpy },
        { provide: NotificationService, useValue: notificationSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ScheduleSessionModal);
    component = fixture.componentInstance;
    mockSessionService = TestBed.inject(
      SessionService
    ) as jasmine.SpyObj<SessionService>;
    mockNotificationService = TestBed.inject(
      NotificationService
    ) as jasmine.SpyObj<NotificationService>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should schedule session', () => {
    mockSessionService.scheduleSession.and.returnValue(
      of(mockScheduledSession)
    );

    const mockSessionForm = {
      sessionName: 'Test',
      date: '2025-01-02',
      startTime: '10:00',
      endTime: '11:00',
      sessionLink: 'abc',
    };

    component.sessionForm.patchValue(mockSessionForm);
    component.onSubmit();

    expect(mockSessionService.scheduleSession).toHaveBeenCalledWith(
      mockSessionForm
    );
    expect(mockNotificationService.addNotification).toHaveBeenCalledWith({
      message: 'Test has been successfully scheduled',
      type: 'success',
    });
  });

  it('should call onSubmit() when form is submitted', () => {
    spyOn(component, 'onSubmit');
    const form = fixture.debugElement.query(By.css('form'));
    form.triggerEventHandler('ngSubmit', null);
    expect(component.onSubmit).toHaveBeenCalled();
  });

  it('should reset form when reset button is clicked', () => {
    spyOn(component.sessionForm, 'reset');
    const resetButton = fixture.debugElement.query(
      By.css('.btn-outline-secondary')
    );
    resetButton.triggerEventHandler('click', null);
    expect(component.sessionForm.reset).toHaveBeenCalled();
  });
});
