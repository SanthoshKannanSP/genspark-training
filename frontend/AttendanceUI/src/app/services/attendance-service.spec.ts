import { TestBed } from '@angular/core/testing';
import { AttendanceService } from './attendance-service';
import { HttpClientService } from './http-client-service';
import { of, throwError } from 'rxjs';
import { AttendanceDetailsModel } from '../models/attendance-details-modal';

describe('AttendanceService', () => {
  let service: AttendanceService;
  let httpClientSpy: jasmine.SpyObj<HttpClientService>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClientService', ['get', 'post']);

    TestBed.configureTestingModule({
      providers: [
        AttendanceService,
        { provide: HttpClientService, useValue: httpClientSpy },
      ],
    });

    service = TestBed.inject(AttendanceService);
  });

  describe('updateAllSessions', () => {
    it('should update attendanceDetails', () => {
      const mockData: AttendanceDetailsModel[] = [
        {
          sessionId: 1,
          sessionName: 'Algebra',
          date: '2025-07-01',
          startTime: '09:00',
          endTime: '10:00',
          status: 'Completed',
          registeredCount: 20,
          attendedCount: 18,
        },
      ];

      const mockResponse = {
        data: {
          pagination: { totalRecords: 1, page: 1, pageSize: 10, totalPages: 1 },
          data: { $values: mockData },
        },
      };

      httpClientSpy.get.and.returnValue(of(mockResponse));

      service.updateAllSessions(1, 10);

      service.attendanceDetails$.subscribe((res) => {
        expect(res.data?.[0].sessionName).toBe('Algebra');
        expect(res.pagination?.totalRecords).toBe(1);
      });

      expect(httpClientSpy.get).toHaveBeenCalledWith(
        '/api/v1/Session/Attendance',
        true,
        jasmine.objectContaining({ params: jasmine.anything() })
      );
    });

    it('should log error if request fails', () => {
      spyOn(console, 'log');
      httpClientSpy.get.and.returnValue(throwError(() => new Error('Error')));
      service.updateAllSessions();

      expect(console.log).toHaveBeenCalledWith(jasmine.any(Error));
    });
  });

  describe('getSessionAttendance', () => {
    it('should update sessionAttendanceDetails', () => {
      const mockResponse = {
        data: {
          pagination: { totalRecords: 1, page: 1, pageSize: 5, totalPages: 1 },
          data: {
            sessionAttendance: {
              $values: [
                {
                  studentId: 1,
                  studentName: 'John Doe',
                  sessionId: 1,
                  attended: true,
                },
              ],
            },
            attendedCount: 1,
            registeredCount: 2,
          },
        },
      };

      httpClientSpy.get.and.returnValue(of(mockResponse));

      service.getSessionAttendance(1);

      service.sessionAttendanceDetails$.subscribe((res) => {
        expect(res.data!.attendedCount).toBe(1);
        expect(res.data!.sessionAttendance.length).toBe(1);
        expect(res.data!.sessionAttendance[0].studentName).toBe('John Doe');
      });
    });

    it('should log error if request fails', () => {
      spyOn(console, 'log');
      httpClientSpy.get.and.returnValue(throwError(() => new Error('Error')));
      service.getSessionAttendance(1);
      expect(console.log).toHaveBeenCalledWith(jasmine.any(Error));
    });
  });

  describe('markAttendance', () => {
    it('should mark attendance', () => {
      const mockResponse = { success: true };
      httpClientSpy.post.and.returnValue(of(mockResponse));

      service.markAttendance(5, 10).subscribe((res) => {
        expect(res).toEqual(mockResponse);
      });

      expect(httpClientSpy.post).toHaveBeenCalledWith(
        '/api/v1/Attendance/Mark',
        { studentId: 5, sessionId: 10 },
        true
      );
    });
  });

  describe('unmarkAttendance', () => {
    it('should unmark attendance', () => {
      const mockResponse = { success: true };
      httpClientSpy.post.and.returnValue(of(mockResponse));

      service.unmarkAttendance(5, 10).subscribe((res) => {
        expect(res).toEqual(mockResponse);
      });

      expect(httpClientSpy.post).toHaveBeenCalledWith(
        '/api/v1/Attendance/Unmark',
        { studentId: 5, sessionId: 10 },
        true
      );
    });
  });
});
