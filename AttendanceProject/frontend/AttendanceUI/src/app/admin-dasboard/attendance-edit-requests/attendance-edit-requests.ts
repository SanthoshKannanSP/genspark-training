import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AttendanceService } from '../../services/attendance-service';
import { AttendanceEditRequestModel } from '../../models/attendance-edit-request-model';
import { NotificationService } from '../../services/notification-service';

@Component({
  selector: 'app-attendance-edit-requests',
  imports: [CommonModule],
  templateUrl: './attendance-edit-requests.html',
  styleUrl: './attendance-edit-requests.css'
})
export class AttendanceEditRequests {
  editRequests: AttendanceEditRequestModel[] = [];
  notificationService = inject(NotificationService);

  constructor(private attendanceService: AttendanceService) {}
  
  ngOnInit() {
  this.attendanceService.getAllAttendanceEditRequests();
  this.attendanceService.attendanceEditRequests$.subscribe(data => {
    console.log('Edit Requests:', data);
    this.editRequests = data;
  });
}
// approveRequest(request: AttendanceEditRequestModel) {
//   if (request.requestedStatus === 'Attended') {
//     this.attendanceService
//       .markAttendance(request.studentId, request.sessionId)
//       .subscribe(() => {
//         console.log('Marked present');
//       });
//   } else if (request.requestedStatus === 'NotAttended') {
//     this.attendanceService
//       .unmarkAttendance(request.studentId, request.sessionId)
//       .subscribe(() => {
//         console.log('Marked absent');
//       });
//   }
// }

approveRequest(request: AttendanceEditRequestModel) {
  const operation$ = request.requestedStatus === 'Attended'
    ? this.attendanceService.markAttendance(request.studentId, request.sessionId)
    : this.attendanceService.unmarkAttendance(request.studentId, request.sessionId);

  operation$.subscribe(() => {
    console.log(`Marked ${request.requestedStatus}`);
    // this.attendanceService
    //   .approveAttendanceEditRequest(request.id)
    //   .subscribe(() => {
    //     console.log('Edit request approved');
    //   });
    this.attendanceService.approveAttendanceEditRequest(request.id).subscribe({
  next: () => {
    this.notificationService.addNotification({
        message: 'Attendance edit request approved.',
        type: 'success',
        });
      },
    });
  });
}


}
