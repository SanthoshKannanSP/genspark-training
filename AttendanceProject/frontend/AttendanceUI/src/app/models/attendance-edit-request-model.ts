export interface AttendanceEditRequestModel {
  id: number;
  sessionAttendanceId: number;
  studentId: number;
  sessionId: number;
  requestedStatus: string;
  status: string;
  requestedAt: string;
  studentName: string;
  sessionName: string;
  currentStatus: string;
}