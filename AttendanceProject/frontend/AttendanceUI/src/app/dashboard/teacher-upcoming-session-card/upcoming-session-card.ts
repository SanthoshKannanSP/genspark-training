import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { SessionModel } from '../../models/session-model';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { HttpClientService } from '../../services/http-client-service';
import { SessionService } from '../../services/session-service';
import { NotificationService } from '../../services/notification-service';
import { Router } from '@angular/router';
import { FormatTimePipe } from '../../misc/format-time-pipe';

@Component({
  selector: 'app-upcoming-session-card',
  imports: [FormatDatePipe, FormatTimePipe],
  templateUrl: './upcoming-session-card.html',
  styleUrl: './upcoming-session-card.css',
})
export class UpcomingSessionCard {
  @Input() session!: SessionModel;
  @Output() viewSession = new EventEmitter<SessionModel>();
  api = inject(HttpClientService);
  sessionService = inject(SessionService);
  notificationService = inject(NotificationService);
  router = inject(Router);

  showDetails() {
    this.viewSession.emit(this.session);
  }

  isSessionLive(): boolean {
    const now = new Date();

    const sessionDate = new Date(this.session.date);
    const isToday =
      now.getFullYear() === sessionDate.getFullYear() &&
      now.getMonth() === sessionDate.getMonth() &&
      now.getDate() === sessionDate.getDate();

    const [startHour, startMinute] = this.session.startTime
      .split(':')
      .map(Number);
    const [endHour, endMinute] = this.session.endTime.split(':').map(Number);

    const startDateTime = new Date(sessionDate);
    startDateTime.setHours(startHour, startMinute, 0, 0);

    const endDateTime = new Date(sessionDate);
    endDateTime.setHours(endHour, endMinute, 0, 0);

    return isToday && now >= startDateTime && now <= endDateTime;
  }

  joinLive() {
    if (this.api.hasRole('Teacher') && this.session.status == 'Scheduled') {
      this.sessionService.makeSessionLive(this.session.sessionId).subscribe({
        next: () => {
          this.notificationService.addNotification({
            message: 'Started Live Session. Joining...',
            type: 'success',
          });
          window.open(this.session.sessionLink!, '_blank');
          this.router.navigateByUrl('/session/live');
        },
        error: (error) => {
          console.log(error);
          this.notificationService.addNotification({
            message: 'Unable to start session',
            type: 'danger',
          });
        },
      });
    } else if (this.api.hasRole('Student') && this.session.status == 'Live') {
      window.open(this.session.sessionLink!, '_blank');
    } else {
      this.notificationService.addNotification({
        message:
          'Teacher has not started the session yet. Please try again after some time',
        type: 'danger',
      });
    }
  }
}
