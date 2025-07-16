import { inject, Injectable } from '@angular/core';
import { HttpClientService } from './http-client-service';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { SessionModel } from '../models/session-model';
import { PaginatedResponse } from '../models/paginated-response';
import { FilterModel } from '../models/filter-model';
import { HttpParams } from '@angular/common/http';
import { ScheduleSessionModel } from '../models/schedule-session-model';

@Injectable()
export class SessionService {
  api = inject(HttpClientService);
  upcomingSessions = new BehaviorSubject<SessionModel[]>([]);
  upcomingSessions$ = this.upcomingSessions.asObservable();
  pastSessions = new BehaviorSubject<SessionModel[]>([]);
  pastSessions$ = this.pastSessions.asObservable();
  allSessions = new BehaviorSubject<PaginatedResponse<SessionModel[]>>(
    new PaginatedResponse<SessionModel[]>()
  );
  allSessions$ = this.allSessions.asObservable();
  currentFilters: FilterModel | null = null;

  updateUpcomingSessions() {
    this.api.get('/api/v1/Session/Upcoming', true).subscribe({
      next: (data: any) =>
        this.upcomingSessions.next(data.data.$values as SessionModel[]),
      error: (error) => console.log(error),
    });
  }

  updatePastSessions() {
    this.api.get('/api/v1/Session/Past', true).subscribe({
      next: (data: any) =>
        this.pastSessions.next(data.data.$values as SessionModel[]),
      error: (error) => console.log(error),
    });
  }

  updateAllSessions(
    page: number | null = null,
    pageSize: number | null = null,
    filters: FilterModel | null = null
  ) {
    if (filters != null) this.currentFilters = filters;
    let params = new HttpParams();

    if (page != null) params = params.set('page', page.toString());
    if (pageSize != null) params = params.set('pageSize', pageSize.toString());

    if (this.currentFilters != null) {
      Object.entries(this.currentFilters).forEach(([key, value]) => {
        if (value !== '' && value !== null && value !== undefined) {
          params = params.set(key, value);
        }
      });
    }
    this.api.get('/api/v1/Session/All', true, { params }).subscribe({
      next: (data: any) => {
        let result = new PaginatedResponse<SessionModel[]>();
        result.pagination = data.data.pagination;
        result.data = data.data.data.$values;
        this.allSessions.next(result);
      },
      error: (error) => console.log(error),
    });
  }

  scheduleSession(session: ScheduleSessionModel) {
    return this.api.post('/api/v1/Session', session, true).pipe(
      tap(() => {
        this.currentFilters = null;
        this.updateAllSessions();
      }),
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  cancelSession(sessionId: number) {
    return this.api.post(`/api/v1/Session/${sessionId}/Cancel`, {}, true).pipe(
      tap(() => {
        this.currentFilters = null;
        this.updateAllSessions();
      }),
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  editSession(sessionId: number, session: SessionModel) {
    return this.api
      .post(`/api/v1/Session/${sessionId}/Update`, session, true)
      .pipe(tap(() => this.updateAllSessions()));
  }

  addStudentToSession(sessionCode: string) {
    return this.api.post(
      '/api/v1/Session/AddStudent',
      { sessionCode: sessionCode },
      true
    );
  }

  makeSessionLive(sessionId: number) {
    return this.api.post(
      '/api/v1/Session/Make/Live',
      { sessionId: sessionId },
      true
    );
  }
}
