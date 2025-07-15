export class SessionAttendanceModel {
  registeredCount: number = 0;
  attendedCount: number = 0;
  sessionAttendance: StudentAttendance[] = [];
}

export class StudentAttendance {
  studentId: number = 0;
  studentName: string = '';
  sessionId: number = 0;
  attended: boolean = false;
}
