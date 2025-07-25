export interface StudentResponseDto {
  studentId: number;
  name: string;
  email: string;
  status: string;
  batch?: {
    batchId: number;
    batchName: string;
  };
}