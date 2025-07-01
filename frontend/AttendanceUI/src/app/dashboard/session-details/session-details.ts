import { Component, inject, Input } from '@angular/core';
import { SessionModel } from '../../models/session-model';
import { HttpClientService } from '../../services/http-client-service';

@Component({
  selector: 'app-session-details',
  imports: [],
  templateUrl: './session-details.html',
  styleUrl: './session-details.css',
})
export class SessionDetails {
  api = inject(HttpClientService);
  session: SessionModel | null = null;
  student!: boolean;
  @Input() modalId!: string;

  constructor() {
    this.student = this.api.hasRole('Student');
  }

  openModal(session: SessionModel) {
    this.session = session;
  }
}
