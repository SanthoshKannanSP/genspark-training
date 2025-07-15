export class LiveSessionModel {
  public sessionId!: number;
  public sessionName!: string;
  public attendingStudents!: LiveSessionStudentsModel[];
  public notJoinedStudents!: LiveSessionStudentsModel[];
}

export class LiveSessionStudentsModel {
  public studentId!: number;
  public studentName!: string;
}
