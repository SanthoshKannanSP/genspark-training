import { Component, inject, Input } from '@angular/core';
import { SessionModel } from '../../models/session-model';
import { AttendanceDetailsModel } from '../../models/attendance-details-modal';
import { PaginatedResponse } from '../../models/paginated-response';
import { AttendanceService } from '../../services/attendance-service';
import { SessionAttendanceModel } from '../../models/session-attendance-modal';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-session-attendance-details-modal',
  imports: [ReactiveFormsModule],
  templateUrl: './session-attendance-details-modal.html',
  styleUrl: './session-attendance-details-modal.css',
})
export class SessionAttendanceDetailsModal {
  session: AttendanceDetailsModel | null = null;
  students = new PaginatedResponse<SessionAttendanceModel>();
  attendanceService = inject(AttendanceService);

  filterForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.attendanceService.sessionAttendanceDetails$.subscribe({
      next: (data) => {
        this.students = data;
      },
      error: (error) => console.log(error),
    });

    this.filterForm = this.fb.group({
      studentName: [''],
      attended: [null],
    });
  }

  toggleAttendance(index: number) {
    this.students.data!.sessionAttendance[index].attended =
      !this.students.data!.sessionAttendance[index].attended;
  }

  openModal(session: AttendanceDetailsModel) {
    this.session = session;
    this.attendanceService.getSessionAttendance(this.session.sessionId);
  }

  applyFilter() {
    this.attendanceService.getSessionAttendance(
      this.session!.sessionId,
      null,
      null,
      this.filterForm.value
    );
  }

  resetFilter() {
    this.filterForm.reset();
    this.applyFilter();
  }

  goToPage(page: number) {
    this.attendanceService.getSessionAttendance(
      this.session!.sessionId,
      page,
      this.students.pagination?.pageSize
    );
  }

  public get pageNumbers(): number[] {
    if (this.students.pagination == null) return [];
    return Array.from(
      { length: this.students.pagination.totalPages },
      (_, i) => i + 1
    );
  }

  public get emptyRows(): number[] {
    if (this.students.pagination == null) return [];
    return Array.from(
      {
        length:
          this.students.pagination.pageSize -
          this.students.data!.sessionAttendance.length,
      },
      (_, i) => i + 1
    );
  }
}
