import { inject, Injectable } from '@angular/core';
import { LiveSessionModel } from '../models/live-session-model';
import { HttpClientService } from './http-client-service';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class LiveSessionService {
  liveSessionDetails = new BehaviorSubject<LiveSessionModel>(
    new LiveSessionModel()
  );
  liveSessionDetails$ = this.liveSessionDetails.asObservable();
  api = inject(HttpClientService);

  updateLiveSession() {
    return this.api.get('/api/v1/Session/Live', true).subscribe({
      next: (data: any) => {
        let liveSession = new LiveSessionModel();
        liveSession.sessionId = data.data.sessionId;
        liveSession.sessionName = data.data.sessionName;
        liveSession.attendingStudents = data.data.attendingStudents.$values;
        liveSession.notJoinedStudents = data.data.notJoinedStudents.$values;
        this.liveSessionDetails.next(liveSession);
      },
      error: (error) => console.log(error),
    });
  }

  completeLiveSession(sessionId: number) {
    return this.api.post(`/api/v1/Session/${sessionId}/Complete`, {}, true);
  }
}
