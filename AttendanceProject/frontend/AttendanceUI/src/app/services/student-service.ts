import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client-service';


@Injectable({ providedIn: 'root' })
export class StudentService {
  constructor(private http: HttpClientService) {}

  getAllStudents() {
    return this.http.get('/api/v1/Student', true);
  }
}
