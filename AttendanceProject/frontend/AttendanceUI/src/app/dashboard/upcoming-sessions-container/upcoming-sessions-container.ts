import { Component, inject, ViewChild } from '@angular/core';
import { UpcomingSessionCard } from '../teacher-upcoming-session-card/upcoming-session-card';
import { SessionService } from '../../services/session-service';
import { SessionDetails } from '../session-details/session-details';
import { SessionModel } from '../../models/session-model';

@Component({
  selector: 'app-upcoming-sessions-container',
  imports: [UpcomingSessionCard, SessionDetails],
  templateUrl: './upcoming-sessions-container.html',
  styleUrl: './upcoming-sessions-container.css',
})
export class UpcomingSessionsContainer {
  sessionService = inject(SessionService);
  upcomingSessions!: any[];
  @ViewChild('upcomingSessionDetailsModal')
  sessionDetailsModal!: SessionDetails;

  viewSession(session: SessionModel) {
    this.sessionDetailsModal.openModal(session);
  }

  constructor() {
    this.sessionService.upcomingSessions$.subscribe({
      next: (data) => {
        this.upcomingSessions = data as any[];
      },
    });
    this.sessionService.updateUpcomingSessions();
  }
}
