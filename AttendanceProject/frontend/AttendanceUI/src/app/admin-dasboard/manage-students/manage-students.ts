import { Component, OnInit } from '@angular/core';
import { StudentResponseDto } from '../../models/student-response.dto';
import { BatchResponseDto } from '../../models/batch-response.dto';
import { StudentService } from '../../services/student-service';
import { BatchService } from '../../services/batch-service';
import { AssignStudentRequestDto } from '../../models/assign-student-request.dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NotificationService } from '../../services/notification-service';

@Component({
  selector: 'app-manage-students',
  imports:[CommonModule, FormsModule],
  templateUrl: './manage-students.html',
  styleUrl: './manage-students.css',
})
export class ManageStudents implements OnInit {
  students: StudentResponseDto[] = [];
  batches: BatchResponseDto[] = [];
  selectedBatch: { [studentId: number]: number } = {};
  newBatchName: string = '';
  batchCreateMessage: string = '';

  constructor(
    private studentService: StudentService,
    private batchService: BatchService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.loadStudents();
    this.loadBatches();
  }

  loadStudents() {
    this.studentService.getAllStudents().subscribe((res: any) => {
      this.students = res.data.$values;
      console.log(this.students);
    });
  }

  loadBatches() {
    this.batchService.getAllBatches().subscribe((res: any) => {
      this.batches = res.data.$values;
    });
  }

  // assignBatch(studentId: number) {
  //   const batchId = this.selectedBatch[studentId];
  //   if (!batchId) return;

  //   const dto: AssignStudentRequestDto = { studentId, batchId };
  //   this.batchService.assignStudentToBatch(dto).subscribe(() => {
  //     this.loadStudents(); 
  //   });
  // }
  // createBatch() {
  // if (!this.newBatchName.trim()) {
  //   alert("Please enter a batch name.");
  //   return;
  // }

  //   this.batchService.createBatch(this.newBatchName).subscribe({
  //     next: (res: any) => {
  //     this.batchCreateMessage = `Batch '${this.newBatchName}' created successfully.`;
  //       this.newBatchName = '';
  //       this.loadBatches();
  //       },
  //     error: (err) => {
  //       console.error(err);
  //       this.batchCreateMessage = 'Failed to create batch.';
  //     }
  //   });
  // }

  assignBatch(studentId: number) {
  const batchId = this.selectedBatch[studentId];
  if (!batchId) return;

  const dto: AssignStudentRequestDto = { studentId, batchId };
  this.batchService.assignStudentToBatch(dto).subscribe({
    next: () => {
      this.loadStudents();
      this.notificationService.addNotification({
        message: 'Student successfully assigned to batch.',
        type: 'success',
      });
    },
    error: (err) => {
      console.error(err);
      this.notificationService.addNotification({
        message: 'Failed to assign student to batch.',
        type: 'danger',
      });
    }
  });
}

createBatch() {
  if (!this.newBatchName.trim()) {
    alert("Please enter a batch name.");
    return;
  }

  this.batchService.createBatch(this.newBatchName).subscribe({
    next: (res: any) => {
      this.notificationService.addNotification({
        message: `Batch '${this.newBatchName}' created successfully.`,
        type: 'success',
      });
      this.newBatchName = '';
      this.loadBatches();
    },
    error: (err) => {
      console.error(err);
      this.notificationService.addNotification({
        message: 'Failed to create batch.',
        type: 'danger',
      });
    }
  });
}

}
