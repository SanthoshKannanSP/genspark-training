import { Component, inject, Input, OnInit } from '@angular/core';
import { PaginatedResponse } from '../../models/paginated-response';
import { SessionModel } from '../../models/session-model';
import { SessionService } from '../../services/session-service';
import { EditSessionComponent } from '../edit-session-modal/edit-session-modal';
import { NotificationService } from '../../services/notification-service';
import { HttpClientService } from '../../services/http-client-service';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { FormatTimePipe } from '../../misc/format-time-pipe';
import { SessionNotesModal } from '../../session-notes-modal/session-notes-modal';

@Component({
  selector: 'app-sessions-table',
  imports: [
    EditSessionComponent,
    FormatDatePipe,
    FormatTimePipe,
    SessionNotesModal,
  ],
  templateUrl: './sessions-table.html',
  styleUrl: './sessions-table.css',
})
export class SessionsTable {
  api = inject(HttpClientService);
  sessionService = inject(SessionService);
  sessions = new PaginatedResponse<SessionModel[]>();
  notificationService = inject(NotificationService);
  student: boolean;

  constructor() {
    this.sessionService.allSessions$.subscribe({
      next: (data) => (this.sessions = data),
      error: (error) => console.log(error),
    });
    this.sessionService.updateAllSessions();
    this.student = this.api.hasRole('Student');
  }

  goToPage(page: number) {
    this.sessionService.updateAllSessions(
      page,
      this.sessions.pagination?.pageSize
    );
  }

  copySessionInviteToClipboard(value: string) {
    navigator.clipboard
      .writeText(`http://localhost:4200/invite/${value}`)
      .then(() => {
        this.notificationService.addNotification({
          message: 'Invite link copied to the clipboard',
          type: 'success',
        });
      })
      .catch((err) => {
        this.notificationService.addNotification({
          message: 'Unable to copy link to clipboard',
          type: 'danger',
        });
      });
  }

  isScheduled(index: number) {
    if (this.sessions.data![index].status == 'Scheduled') return true;
    return false;
  }

  cancelSession(sessionId: number) {
    if (
      confirm(
        'Are you sure you want to cancel this session? This action cannot be undone'
      )
    ) {
      this.sessionService.cancelSession(sessionId).subscribe({
        next: (data) => {
          this.notificationService.addNotification({
            message: 'Session cancelled successfully',
            type: 'success',
          });
        },
        error: (error) => {
          console.log(error);
          this.notificationService.addNotification({
            message: 'Unable to cancel session. Please try again later',
            type: 'danger',
          });
        },
      });
    }
  }

  public get pageNumbers(): number[] {
    if (this.sessions.pagination == null) return [];
    return Array.from(
      { length: this.sessions.pagination.totalPages },
      (_, i) => i + 1
    );
  }

  public get emptyRows(): number[] {
    if (this.sessions.pagination == null) return [];
    return Array.from(
      {
        length: this.sessions.pagination.pageSize - this.sessions.data!.length,
      },
      (_, i) => i + 1
    );
  }
}
