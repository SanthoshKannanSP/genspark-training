import { inject, Injectable } from '@angular/core';
import { HttpClientService } from './http-client-service';
import { BehaviorSubject } from 'rxjs';
import { SessionNotesModel } from '../models/session-notes-model';

@Injectable()
export class NoteService {
  api = inject(HttpClientService);
  notes = new BehaviorSubject<SessionNotesModel[]>([]);
  notes$ = this.notes.asObservable();

  uploadNotes(files: FileList, sessionId: number) {
    for (let file of files) {
      const formData = new FormData();
      formData.append('file', file);
      formData.append('sessionId', sessionId.toString());
      this.api.post('/api/v1/Note', formData, true).subscribe({
        next: (data) => this.getSessionNote(sessionId),
        error: (error) => console.log(error),
      });
    }
  }

  getNote(noteId: number) {
    return this.api.get(`/api/v1/Note/${noteId}`, true, {}, 'blob');
  }

  getSessionNote(sessionId: number) {
    this.api.get(`/api/v1/Note/Session/${sessionId}`).subscribe({
      next: (data: any) => this.notes.next(data.data.$values),
      error: (error) => console.log(error),
    });
  }

  deleteNote(noteId: number) {
    return this.api.delete(`/api/v1/Note/${noteId}`, true);
  }
}
