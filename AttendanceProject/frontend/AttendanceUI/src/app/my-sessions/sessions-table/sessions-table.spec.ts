import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionsTable } from './sessions-table';
import { PaginatedResponse } from '../../models/paginated-response';
import { SessionModel, TeacherDetails } from '../../models/session-model';
import { of } from 'rxjs';
import { HttpClientService } from '../../services/http-client-service';
import { SessionService } from '../../services/session-service';
import { NotificationService } from '../../services/notification-service';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { FormatTimePipe } from '../../misc/format-time-pipe';
import { DatePipe } from '@angular/common';
import { SettingsService } from '../../services/settings-service';

describe('SessionsTable', () => {
  let component: SessionsTable;
  let fixture: ComponentFixture<SessionsTable>;
  let mockHttpClientService: jasmine.SpyObj<HttpClientService>;
  let mockSessionService: jasmine.SpyObj<SessionService>;
  let mockNotificationService: jasmine.SpyObj<NotificationService>;

  beforeEach(async () => {
    const apiSpy = jasmine.createSpyObj('HttpClientService', ['hasRole']);

    const mockAllSessions: PaginatedResponse<SessionModel[]> = {
      data: [
        {
          sessionId: 1,
          sessionName: 'Test',
          date: '2025-01-02',
          startTime: '10:00',
          endTime: '11:00',
          sessionCode: 'abc',
          sessionLink: 'abc',
          status: 'Completed',
          teacherDetails: new TeacherDetails(),
          attended: true,
        },
      ],
      pagination: {
        page: 1,
        pageSize: 1,
        totalPages: 2,
        totalRecords: 2,
      },
    };
    const sessionSpy = jasmine.createSpyObj(
      'SessionService',
      ['updateAllSessions'],
      {
        allSessions$: of(mockAllSessions),
      }
    );
    const notificationSpy = jasmine.createSpyObj('NotificationService', [
      'addNotification',
    ]);
    const settingsSpy = {
      getDateFormat: jasmine
        .createSpy('getDateFormat')
        .and.returnValue('dd/MM/yyyy'),
      getTimeFormat: jasmine
        .createSpy('getTimeFormat')
        .and.returnValue('hh:mm aa'),
    };

    await TestBed.configureTestingModule({
      imports: [SessionsTable, FormatDatePipe, FormatTimePipe],
      providers: [
        { provide: HttpClientService, useValue: apiSpy },
        { provide: SessionService, useValue: sessionSpy },
        { provide: NotificationService, useValue: notificationSpy },
        { provide: SettingsService, useValue: settingsSpy },
        { provide: DatePipe },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(SessionsTable);
    component = fixture.componentInstance;

    mockHttpClientService = TestBed.inject(
      HttpClientService
    ) as jasmine.SpyObj<HttpClientService>;
    mockNotificationService = TestBed.inject(
      NotificationService
    ) as jasmine.SpyObj<NotificationService>;
    mockSessionService = TestBed.inject(
      SessionService
    ) as jasmine.SpyObj<SessionService>;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should copy invite link to clipboard and show notification', async () => {
    spyOn(navigator.clipboard, 'writeText').and.returnValue(Promise.resolve());
    await component.copySessionInviteToClipboard('abcdefg');
    expect(navigator.clipboard.writeText).toHaveBeenCalledWith(
      'http://localhost:4200/invite/abcdefg'
    );
    expect(mockNotificationService.addNotification).toHaveBeenCalledWith({
      message: 'Invite link copied to the clipboard',
      type: 'success',
    });
  });

  it('show danger notification on error', async () => {
    spyOn(navigator.clipboard, 'writeText').and.returnValue(
      Promise.reject(new Error('Clipboard Error'))
    );
    await component.copySessionInviteToClipboard('abcdefg');
    expect(navigator.clipboard.writeText).toHaveBeenCalledWith(
      'http://localhost:4200/invite/abcdefg'
    );
  });

  it('should call goToPage', () => {
    component.goToPage(2);
    expect(mockSessionService.updateAllSessions).toHaveBeenCalledWith(2, 1);
  });
});
