import { Component, inject, ViewChild } from '@angular/core';
import { PastSessionCard } from '../past-session-card/past-session-card';
import { SessionService } from '../../services/session-service';
import { SessionModel } from '../../models/session-model';
import { SessionDetails } from '../session-details/session-details';
import { SessionAttendanceDetailsModal } from '../../attendance-details/session-attendance-details-modal/session-attendance-details-modal';

@Component({
  selector: 'app-past-sessions-container',
  imports: [PastSessionCard, SessionDetails, SessionAttendanceDetailsModal],
  templateUrl: './past-sessions-container.html',
  styleUrl: './past-sessions-container.css',
})
export class PastSessionsContainer {
  sessionService = inject(SessionService);
  pastSessions!: SessionModel[];
  @ViewChild('pastSessionDetailsModal') sessionDetailsModal!: SessionDetails;
  @ViewChild('pastSessionDetailsModal')
  SessionAttendanceDetailsModal!: SessionAttendanceDetailsModal;

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

  // viewAttendance(session: SessionModel) {
  //   this.SessionAttendanceDetailsModal.openModal(session);
  // }
}
