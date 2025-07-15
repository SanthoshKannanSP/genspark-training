import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { LiveSessionService } from './live-session-service';
import { HttpClientService } from './http-client-service';
import { Environment } from '../../environment/environment';
import { LiveSessionModel } from '../models/live-session-model';
import { of, throwError } from 'rxjs';

describe('LiveSessionService', () => {
  let service: LiveSessionService;
  let httpClientSpy: jasmine.SpyObj<HttpClientService>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClientService', ['get', 'post']);

    TestBed.configureTestingModule({
      providers: [
        LiveSessionService,
        { provide: HttpClientService, useValue: httpClientSpy },
      ],
    });

    service = TestBed.inject(LiveSessionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('updateLiveSession', () => {
    it('should fetch live session details', () => {
      let emittedSession: LiveSessionModel | undefined;
      const mockLiveSessionResponse = {
        data: {
          sessionId: 1,
          sessionName: 'Math Class',
          attendingStudents: { $values: [{ id: 1, name: 'Alice' }] },
          notJoinedStudents: { $values: [{ id: 2, name: 'Bob' }] },
        },
      };

      httpClientSpy.get.and.returnValue(of(mockLiveSessionResponse));
      service.updateLiveSession();

      service.liveSessionDetails$.subscribe((session) => {
        emittedSession = session;
      });

      expect(httpClientSpy.get).toHaveBeenCalledWith(
        '/api/v1/Session/Live',
        true
      );
      expect(emittedSession?.sessionId).toBe(1);
      expect(emittedSession?.sessionName).toBe('Math Class');
      expect(emittedSession?.attendingStudents.length).toBe(1);
      expect(emittedSession?.notJoinedStudents.length).toBe(1);
    });

    it('should log error if request fails', () => {
      spyOn(console, 'log');
      httpClientSpy.get.and.returnValue(throwError(() => new Error('Error')));

      service.updateLiveSession();

      expect(console.log).toHaveBeenCalledWith(jasmine.any(Error));
    });
  });

  describe('completeLiveSession', () => {
    it('should complete session', () => {
      const mockResponse = { success: true };
      httpClientSpy.post.and.returnValue(of(mockResponse));

      service.completeLiveSession(42).subscribe((res) => {
        expect(res).toEqual(mockResponse);
      });

      expect(httpClientSpy.post).toHaveBeenCalledWith(
        '/api/v1/Session/42/Complete',
        {},
        true
      );
    });
  });
});
