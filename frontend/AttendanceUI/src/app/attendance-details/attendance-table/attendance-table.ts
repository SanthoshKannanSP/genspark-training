import { Component, ElementRef, inject, Input, ViewChild } from '@angular/core';
import { AttendanceService } from '../../services/attendance-service';
import { PaginatedResponse } from '../../models/paginated-response';
import { AttendanceDetailsModel } from '../../models/attendance-details-modal';
import { SessionAttendanceDetailsModal } from '../session-attendance-details-modal/session-attendance-details-modal';
import { FormatDatePipe } from '../../misc/format-date-pipe';

@Component({
  selector: 'app-attendance-table',
  imports: [SessionAttendanceDetailsModal, FormatDatePipe],
  templateUrl: './attendance-table.html',
  styleUrl: './attendance-table.css',
})
export class AttendanceTable {
  attendanceService = inject(AttendanceService);
  attendances = new PaginatedResponse<AttendanceDetailsModel[]>();
  @ViewChild('sessionAttendanceModal')
  sessionAttendanceModal!: SessionAttendanceDetailsModal;

  constructor() {
    this.attendanceService.attendanceDetails$.subscribe({
      next: (data) => {
        this.attendances = data;
      },
      error: (error) => console.log(error),
    });
    this.attendanceService.updateAllSessions();
  }

  goToPage(page: number) {
    this.attendanceService.updateAllSessions(
      page,
      this.attendances.pagination?.pageSize
    );
  }

  openModal(session: AttendanceDetailsModel) {
    this.sessionAttendanceModal.openModal(session);
  }

  public get pageNumbers(): number[] {
    if (this.attendances.pagination == null) return [];
    return Array.from(
      { length: this.attendances.pagination.totalPages },
      (_, i) => i + 1
    );
  }

  public get emptyRows(): number[] {
    if (this.attendances.pagination == null) return [];
    return Array.from(
      {
        length:
          this.attendances.pagination.pageSize - this.attendances.data!.length,
      },
      (_, i) => i + 1
    );
  }
}
