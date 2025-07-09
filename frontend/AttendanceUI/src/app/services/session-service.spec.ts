import { TestBed } from '@angular/core/testing';
import { SessionService } from './session-service';
import { HttpClientService } from './http-client-service';
import { of } from 'rxjs';
import { SessionModel } from '../models/session-model';
import { ScheduleSessionModel } from '../models/schedule-session-model';
import { PaginatedResponse } from '../models/paginated-response';
import { HttpParams } from '@angular/common/http';

describe('SessionService', () => {
  let service: SessionService;
  let httpClientSpy: jasmine.SpyObj<HttpClientService>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClientService', ['get', 'post']);

    TestBed.configureTestingModule({
      providers: [
        SessionService,
        { provide: HttpClientService, useValue: httpClientSpy },
      ],
    });

    service = TestBed.inject(SessionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('updateUpcomingSessions', () => {
    it('should get upcoming sessions', () => {
      const mockResponse = {
        data: {
          $values: [
            {
              sessionId: 1,
              sessionName: 'Test Session',
              sessionLink: '',
              date: '2024-01-01',
              startTime: '10:00',
              endTime: '11:00',
              status: 'Scheduled',
            },
          ],
        },
      };

      httpClientSpy.get.and.returnValue(of(mockResponse));

      service.updateUpcomingSessions();

      service.upcomingSessions$.subscribe((sessions) => {
        expect(sessions.length).toBe(1);
        expect(sessions[0].sessionName).toBe('Test Session');
      });

      expect(httpClientSpy.get).toHaveBeenCalledWith(
        '/api/v1/Session/Upcoming',
        true
      );
    });
  });

  describe('updatePastSessions', () => {
    it('should get past sessions', () => {
      const mockResponse = {
        data: {
          $values: [
            {
              sessionId: 2,
              sessionName: 'Past Session',
              sessionLink: '',
              date: '2023-01-01',
              startTime: '09:00',
              endTime: '10:00',
              status: 'Completed',
            },
          ],
        },
      };

      httpClientSpy.get.and.returnValue(of(mockResponse));

      service.updatePastSessions();

      service.pastSessions$.subscribe((sessions) => {
        expect(sessions.length).toBe(1);
        expect(sessions[0].sessionName).toBe('Past Session');
      });

      expect(httpClientSpy.get).toHaveBeenCalledWith(
        '/api/v1/Session/Past',
        true
      );
    });
  });

  describe('updateAllSessions', () => {
    it('should get paginated sessions', () => {
      const mockResponse = {
        data: {
          pagination: {
            page: 1,
            totalPages: 1,
            pageSize: 10,
            totalRecords: 10,
          },
          data: {
            $values: [
              {
                sessionId: 3,
                sessionName: 'All Session',
                sessionLink: '',
                date: '2025-01-01',
                startTime: '13:00',
                endTime: '14:00',
                status: 'Scheduled',
              },
            ],
          },
        },
      };

      httpClientSpy.get.and.returnValue(of(mockResponse));

      service.updateAllSessions(1, 10);

      service.allSessions$.subscribe((response) => {
        expect(response.data?.length).toBe(1);
        expect(response.data?.[0].sessionName).toBe('All Session');
        expect(response.pagination?.totalRecords).toBe(10);
      });

      expect(httpClientSpy.get).toHaveBeenCalledWith(
        '/api/v1/Session/All',
        true,
        {
          params: jasmine.any(HttpParams),
        }
      );
    });
  });

  describe('scheduleSession', () => {
    it('should schedule new session and update all sessions', () => {
      const mockSession: ScheduleSessionModel = {
        sessionName: 'Scheduled Session',
        date: '2025-01-01',
        startTime: '12:00',
        endTime: '13:00',
      };

      httpClientSpy.post.and.returnValue(of({}));

      spyOn(service, 'updateAllSessions');

      service.scheduleSession(mockSession).subscribe();

      expect(httpClientSpy.post).toHaveBeenCalledWith(
        '/api/v1/Session',
        mockSession,
        true
      );
      expect(service.updateAllSessions).toHaveBeenCalled();
    });
  });

  describe('editSession', () => {
    it('should update session', () => {
      const sessionId = 42;
      const session: SessionModel = {
        sessionId: 42,
        sessionName: 'Updated Session',
        sessionLink: '',
        date: '2025-01-02',
        startTime: '14:00',
        endTime: '15:00',
        status: 'Updated',
        sessionCode: null,
        attended: false,
        teacherDetails: null,
      };

      httpClientSpy.post.and.returnValue(of({}));
      spyOn(service, 'updateAllSessions');

      service.editSession(sessionId, session).subscribe();

      expect(httpClientSpy.post).toHaveBeenCalledWith(
        `/api/v1/Session/${sessionId}/Update`,
        session,
        true
      );
      expect(service.updateAllSessions).toHaveBeenCalled();
    });
  });

  describe('addStudentToSession', () => {
    it('should add student to session', () => {
      httpClientSpy.post.and.returnValue(of({ success: true }));
      service.addStudentToSession('ABC123').subscribe((res: any) => {
        expect(res.success).toBeTrue();
      });
      expect(httpClientSpy.post).toHaveBeenCalledWith(
        '/api/v1/Session/AddStudent',
        { sessionCode: 'ABC123' },
        true
      );
    });
  });

  describe('makeSessionLive', () => {
    it('should make session live', () => {
      httpClientSpy.post.and.returnValue(of({ success: true }));
      service.makeSessionLive(99).subscribe((res: any) => {
        expect(res.success).toBeTrue();
      });
      expect(httpClientSpy.post).toHaveBeenCalledWith(
        '/api/v1/Session/Make/Live',
        { sessionId: 99 },
        true
      );
    });
  });
});
