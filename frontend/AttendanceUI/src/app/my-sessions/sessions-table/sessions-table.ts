import { Component, inject, Input, OnInit } from '@angular/core';
import { PaginatedResponse } from '../../models/paginated-response';
import { SessionModel } from '../../models/session-model';
import { SessionService } from '../../services/session-service';

@Component({
  selector: 'app-sessions-table',
  imports: [],
  templateUrl: './sessions-table.html',
  styleUrl: './sessions-table.css',
})
export class SessionsTable {
  sessionService = inject(SessionService);
  allSessions = new PaginatedResponse<SessionModel[]>();
  constructor() {
    this.sessionService.allSessions$.subscribe({
      next: (data) => (this.allSessions = data),
      error: (error) => console.log(error),
    });
    this.sessionService.updateAllSessions();
  }

  goToPage(page: number) {
    this.sessionService.updateAllSessions(
      page,
      this.allSessions.pagination?.pageSize
    );
  }

  public get pageNumbers(): number[] {
    if (this.allSessions.pagination == null) return [];
    return Array.from(
      { length: this.allSessions.pagination.totalPages },
      (_, i) => i + 1
    );
  }

  public get emptyRows(): number[] {
    if (this.allSessions.pagination == null) return [];
    return Array.from(
      {
        length:
          this.allSessions.pagination.pageSize - this.allSessions.data!.length,
      },
      (_, i) => i + 1
    );
  }
}
