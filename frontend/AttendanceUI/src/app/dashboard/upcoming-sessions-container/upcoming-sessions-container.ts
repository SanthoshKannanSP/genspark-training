import { Component, inject } from '@angular/core';
import { UpcomingSessionCard } from '../upcoming-session-card/upcoming-session-card';
import { SessionService } from '../../services/session-service';

@Component({
  selector: 'app-upcoming-sessions-container',
  imports: [UpcomingSessionCard],
  templateUrl: './upcoming-sessions-container.html',
  styleUrl: './upcoming-sessions-container.css',
})
export class UpcomingSessionsContainer {
  sessionService = inject(SessionService);
  upcomingSessions!: any[];

  constructor() {
    this.sessionService.upcomingSessions$.subscribe({
      next: (data) => {
        this.upcomingSessions = data as any[];
        console.log(data);
      },
    });
    this.sessionService.updateUpcomingSessions();
  }
}
