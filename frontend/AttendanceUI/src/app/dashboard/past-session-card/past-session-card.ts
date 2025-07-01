import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { HttpClientService } from '../../services/http-client-service';
import { SessionModel } from '../../models/session-model';
import { SessionDetails } from '../session-details/session-details';

@Component({
  selector: 'app-past-session-card',
  imports: [SessionDetails],
  templateUrl: './past-session-card.html',
  styleUrl: './past-session-card.css',
})
export class PastSessionCard {
  @Input() session!: SessionModel;
  @Output() viewSession = new EventEmitter<SessionModel>();

  showDetails() {
    this.viewSession.emit(this.session);
  }

  api = inject(HttpClientService);
  student!: boolean;

  constructor() {
    this.student = this.api.hasRole('Student');
  }
}
