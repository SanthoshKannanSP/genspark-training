import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { HttpClientService } from '../../services/http-client-service';
import { SessionModel } from '../../models/session-model';
import { SessionDetails } from '../session-details/session-details';
import { FormatDatePipe } from '../../misc/format-date-pipe';

@Component({
  selector: 'app-past-session-card',
  imports: [SessionDetails, FormatDatePipe],
  templateUrl: './past-session-card.html',
  styleUrl: './past-session-card.css',
})
export class PastSessionCard {
  @Input() session!: SessionModel;
  @Output() viewSession = new EventEmitter<SessionModel>();

  showDetails() {
    this.viewSession.emit(this.session);
  }

  showAttendance() {}

  api = inject(HttpClientService);
  student!: boolean;

  constructor() {
    this.student = this.api.hasRole('Student');
  }
}
