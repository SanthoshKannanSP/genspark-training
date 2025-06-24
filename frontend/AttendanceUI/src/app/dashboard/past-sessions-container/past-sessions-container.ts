import { Component, inject } from '@angular/core';
import { PastSessionCard } from '../past-session-card/past-session-card';
import { SessionService } from '../../services/session-service';
import { SessionModel } from '../../models/session-model';

@Component({
  selector: 'app-past-sessions-container',
  imports: [PastSessionCard],
  templateUrl: './past-sessions-container.html',
  styleUrl: './past-sessions-container.css',
})
export class PastSessionsContainer {
  sessionService = inject(SessionService);
  pastSessions!: SessionModel[];

  constructor() {
    this.sessionService.pastSessions$.subscribe({
      next: (data) => {
        this.pastSessions = data as SessionModel[];
        console.log(data);
      },
    });
    this.sessionService.updatePastSessions();
  }
}
