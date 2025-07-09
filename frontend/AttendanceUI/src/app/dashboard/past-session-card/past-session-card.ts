import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { HttpClientService } from '../../services/http-client-service';
import { SessionModel } from '../../models/session-model';
import { SessionDetails } from '../session-details/session-details';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { FormatTimePipe } from '../../misc/format-time-pipe';
import { AttendanceService } from '../../services/attendance-service';

@Component({
  selector: 'app-past-session-card',
  imports: [SessionDetails, FormatDatePipe, FormatTimePipe],
  templateUrl: './past-session-card.html',
  styleUrl: './past-session-card.css',
})
export class PastSessionCard {
  @Input() session!: SessionModel;
  @Output() viewSession = new EventEmitter<SessionModel>();
  attendanceService = inject(AttendanceService);

  showDetails() {
    this.viewSession.emit(this.session);
  }

  showAttendance() {}

  api = inject(HttpClientService);
  student!: boolean;

  constructor() {
    this.student = this.api.hasRole('Student');
  }

  exportAttendance() {
    this.attendanceService
      .generateAttendanceReport(this.session.sessionId)
      .subscribe({
        next: (blob) => {
          const url = window.URL.createObjectURL(blob as Blob);
          window.open(url, '_blank');
        },
        error: (error) => console.log(error),
      });
  }
}
