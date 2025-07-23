import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClientService } from './http-client-service';

@Injectable({ providedIn: 'root' })
export class TeacherService {
  constructor(private api: HttpClientService) {}

  getAllTeachers(page: number = 1, pageSize: number = 50): Observable<any> {
    return this.api.get(`/api/v1/Teacher?page=${page}&pageSize=${pageSize}`, true);
  }

  deactivateTeacher(teacherId: number): Observable<any> {
  return this.api.delete(`/api/v1/Teacher/${teacherId}`, true);
}

}
