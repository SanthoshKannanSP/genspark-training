import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FilterMenu } from '../filter-menu/filter-menu';
import { SessionsTable } from '../sessions-table/sessions-table';
import { ScheduleSessionModal } from '../schedule-session-modal/schedule-session-modal';
import { HttpClientService } from '../../services/http-client-service';

@Component({
  selector: 'app-my-sessions-page',
  imports: [FilterMenu, SessionsTable, ScheduleSessionModal],
  templateUrl: './my-sessions-page.html',
  styleUrl: './my-sessions-page.css',
})
export class MySessionsPage {
  api = inject(HttpClientService);
}
