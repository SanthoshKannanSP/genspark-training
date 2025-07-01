import { inject, Injectable } from '@angular/core';
import { HttpClientService } from './http-client-service';
import { BehaviorSubject } from 'rxjs';
import { FilterModel } from '../models/filter-model';
import { HttpParams } from '@angular/common/http';
import { PaginatedResponse } from '../models/paginated-response';
import { AttendanceDetailsModel } from '../models/attendance-details-modal';
import { SessionAttendanceModel } from '../models/session-attendance-modal';

@Injectable()
export class AttendanceService {
  api = inject(HttpClientService);
  attendanceDetails = new BehaviorSubject<
    PaginatedResponse<AttendanceDetailsModel[]>
  >(new PaginatedResponse<AttendanceDetailsModel[]>());
  attendanceDetails$ = this.attendanceDetails.asObservable();
  sessionAttendanceDetails = new BehaviorSubject<
    PaginatedResponse<SessionAttendanceModel>
  >(new PaginatedResponse<SessionAttendanceModel>());
  sessionAttendanceDetails$ = this.sessionAttendanceDetails.asObservable();
  currentFilters: FilterModel | null = null;

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
    this.api.get('/api/v1/Session/Attendance', true, { params }).subscribe({
      next: (data: any) => {
        let result = new PaginatedResponse<AttendanceDetailsModel[]>();
        result.pagination = data.data.pagination;
        result.data = data.data.data.$values;
        this.attendanceDetails.next(result);
      },
      error: (error) => console.log(error),
    });
  }

  getSessionAttendance(
    sessionId: number,
    page: number | null = null,
    pageSize: number | null = null,
    filters: any | null = null
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
    this.api
      .get(`/api/v1/Attendance/Session/${sessionId}`, true, { params })
      .subscribe({
        next: (data: any) => {
          let result = new PaginatedResponse<SessionAttendanceModel>();
          let sessionAttendance = new SessionAttendanceModel();
          sessionAttendance.sessionAttendance =
            data.data.data.sessionAttendance.$values;
          sessionAttendance.attendedCount = data.data.data.attendedCount;
          sessionAttendance.registeredCount = data.data.data.registeredCount;
          result.pagination = data.data.pagination;
          result.data = sessionAttendance;
          this.sessionAttendanceDetails.next(result);
        },
        error: (error) => console.log(error),
      });
  }
}
