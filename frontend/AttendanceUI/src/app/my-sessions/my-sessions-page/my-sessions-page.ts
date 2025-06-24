import { Component, inject } from '@angular/core';
import { FilterMenu } from '../filter-menu/filter-menu';
import { SessionsTable } from '../sessions-table/sessions-table';
import { SessionService } from '../../services/session-service';
import { PaginatedResponse } from '../../models/paginated-response';
import { SessionModel } from '../../models/session-model';

@Component({
  selector: 'app-my-sessions-page',
  imports: [FilterMenu, SessionsTable],
  templateUrl: './my-sessions-page.html',
  styleUrl: './my-sessions-page.css',
})
export class MySessionsPage {}
