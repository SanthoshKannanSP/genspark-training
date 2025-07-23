import { Injectable } from '@angular/core';
import { BatchResponseDto } from '../models/batch-response.dto';
import { AssignStudentRequestDto } from '../models/assign-student-request.dto';
import { HttpClientService } from './http-client-service';

@Injectable({ providedIn: 'root' })
export class BatchService {
  constructor(private http: HttpClientService) {}

  getAllBatches() {
    return this.http.get('/api/v1/Batch', true);
  }

  assignStudentToBatch(dto: AssignStudentRequestDto) {
    return this.http.post('/api/v1/Batch/assign-student', dto, true);
  }
  createBatch(batchName: string) {
    const body = { batchName };
    return this.http.post('/api/v1/Batch', body, true);
  }
}