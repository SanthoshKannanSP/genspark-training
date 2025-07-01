import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SessionModel } from '../../models/session-model';

@Component({
  selector: 'app-upcoming-session-card',
  imports: [],
  templateUrl: './upcoming-session-card.html',
  styleUrl: './upcoming-session-card.css',
})
export class UpcomingSessionCard {
  @Input() session!: SessionModel;
  @Output() viewSession = new EventEmitter<SessionModel>();

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
    if (this.session.status == 'Scheduled' || this.session.status == 'Live') {
      window.open(this.session.sessionLink!, '_blank');
    }
  }
}
