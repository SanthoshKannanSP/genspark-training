import { Component, inject, Input } from '@angular/core';
import { SessionModel } from '../../models/session-model';
import { AttendanceDetailsModel } from '../../models/attendance-details-modal';
import { PaginatedResponse } from '../../models/paginated-response';
import { AttendanceService } from '../../services/attendance-service';
import {
  SessionAttendanceModel,
  StudentAttendance,
} from '../../models/session-attendance-modal';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { NotificationService } from '../../services/notification-service';
import { FormatTimePipe } from '../../misc/format-time-pipe';

@Component({
  selector: 'app-session-attendance-details-modal',
  imports: [ReactiveFormsModule, FormatDatePipe, FormatTimePipe],
  templateUrl: './session-attendance-details-modal.html',
  styleUrl: './session-attendance-details-modal.css',
})
export class SessionAttendanceDetailsModal {
  session: AttendanceDetailsModel | null = null;
  students = new PaginatedResponse<SessionAttendanceModel>();
  attendanceService = inject(AttendanceService);
  notificationService = inject(NotificationService);
  @Input() modalId!: string;

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

  // toggleAttendance(event: Event, student: StudentAttendance, index: number) {
  //   const checkbox = event.target as HTMLInputElement;
  //   const currentAttended = student.attended;
  //   const changeAttended = checkbox.checked;

  //   student.attended = changeAttended;

  //   let studentId = student.studentId;
  //   let sessionId = student.sessionId;
  //   if (currentAttended) {
  //     this.attendanceService.unmarkAttendance(studentId, sessionId).subscribe({
  //       next: (data) => {
  //         const message = `Successfully unmarked attendance to ${this.students.data?.sessionAttendance[index].studentName} for session ${this.session?.sessionName}`;
  //         this.notificationService.addNotification({
  //           message: message,
  //           type: 'success',
  //         });
  //       },
  //       error: (error) => {
  //         console.log(error);
  //         student.attended = currentAttended;
  //         checkbox.checked = currentAttended;
  //         const message = `Failed to unmark attendance to ${this.students.data?.sessionAttendance[index].studentName} for session ${this.session?.sessionName}`;
  //         this.notificationService.addNotification({
  //           message: message,
  //           type: 'danger',
  //         });
  //       },
  //     });
  //   } else {
  //     this.attendanceService.markAttendance(studentId, sessionId).subscribe({
  //       next: (data) => {
  //         console.log(data);
  //         const message = `Successfully marked attendance to ${this.students.data?.sessionAttendance[index].studentName} for session ${this.session?.sessionName}`;
  //         this.notificationService.addNotification({
  //           message: message,
  //           type: 'success',
  //         });
  //       },
  //       error: (error) => {
  //         console.log(error);
  //         student.attended = currentAttended;
  //         checkbox.checked = currentAttended;
  //         const message = `Failed to mark attendance to ${this.students.data?.sessionAttendance[index].studentName} for session ${this.session?.sessionName}`;
  //         this.notificationService.addNotification({
  //           message: message,
  //           type: 'danger',
  //         });
  //       },
  //     });
  //   }
  // }

  // E D I T   R E Q U E S T
  toggleAttendance(event: Event, student: StudentAttendance, index: number) {
    const checkbox = event.target as HTMLInputElement;
    const currentAttended = student.attended;
    const changeAttended = checkbox.checked;

    student.attended = changeAttended;

    const studentId = student.studentId;
    const sessionId = student.sessionId;
    const sessionAttendanceId = student.sessionAttendanceId;

    // Send edit request instead of toggling directly
    const requestedStatus = changeAttended ? 'Attended' : 'NotAttended';

    this.attendanceService.submitEditRequest({
      sessionAttendanceId,
      requestedStatus,
    }).subscribe({
      next: () => {
        this.notificationService.addNotification({
          message: `Attendance edit request submitted for ${this.students.data?.sessionAttendance[index].studentName}`,
          type: 'info',
        });
      },
      error: (error: any) => {
        console.error(error);
        student.attended = currentAttended;
        checkbox.checked = currentAttended;
        this.notificationService.addNotification({
          message: `Failed to submit edit request.`,
          type: 'danger',
        });
      }
    });
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
