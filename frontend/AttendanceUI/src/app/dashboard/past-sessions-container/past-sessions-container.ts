import { Component, inject, ViewChild } from '@angular/core';
import { PastSessionCard } from '../past-session-card/past-session-card';
import { SessionService } from '../../services/session-service';
import { SessionModel } from '../../models/session-model';
import { SessionDetails } from '../session-details/session-details';

@Component({
  selector: 'app-past-sessions-container',
  imports: [PastSessionCard, SessionDetails],
  templateUrl: './past-sessions-container.html',
  styleUrl: './past-sessions-container.css',
})
export class PastSessionsContainer {
  sessionService = inject(SessionService);
  pastSessions!: SessionModel[];
  @ViewChild('pastSessionDetailsModal') sessionDetailsModal!: SessionDetails;

  constructor() {
    this.sessionService.pastSessions$.subscribe({
      next: (data) => {
        this.pastSessions = data as SessionModel[];
      },
    });
    this.sessionService.updatePastSessions();
  }

  viewSession(session: SessionModel) {
    this.sessionDetailsModal.openModal(session);
  }
}
