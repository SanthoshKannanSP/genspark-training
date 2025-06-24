import { Component, inject } from '@angular/core';
import { FilterMenu } from '../filter-menu/filter-menu';
import { SessionsTable } from '../sessions-table/sessions-table';
import { SessionService } from '../../services/session-service';
import { PaginatedResponse } from '../../models/paginated-response';
import { SessionModel } from '../../models/session-model';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ScheduleSessionModel } from '../../models/schedule-session-model';

@Component({
  selector: 'app-my-sessions-page',
  imports: [FilterMenu, SessionsTable, ReactiveFormsModule],
  templateUrl: './my-sessions-page.html',
  styleUrl: './my-sessions-page.css',
})
export class MySessionsPage {
  sessionService = inject(SessionService);
  sessionForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.sessionForm = this.fb.group({
      sessionName: ['', Validators.required],
      date: ['', Validators.required],
      startTime: ['', Validators.required],
      endTime: ['', Validators.required],
    });
  }

  onSubmit() {
    this.sessionService.scheduleSession(
      this.sessionForm.value as ScheduleSessionModel
    );
  }
}
