export class SessionModel {
  public sessionId!: number;
  public sessionName!: string;
  public sessionLink!: string;
  public sessionCode: string | null = null;
  public date!: string;
  public startTime!: string;
  public endTime!: string;
  public status!: string;
  public attended?: boolean;
  public teacherDetails: TeacherDetails | null = null;
}

export class TeacherDetails {
  public teacherId: number | null = null;
  public teacherName: string | null = null;
  public organization: string | null = null;
}
