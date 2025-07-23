// import { Component, OnInit } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import { TeacherService } from '../../services/teacher-service';
// import { TeacherModel } from '../../models/teacher-model';

// @Component({
//   selector: 'app-manage-teachers',
//   imports: [CommonModule],
//   templateUrl: './manage-teachers.html',
//   styleUrl: './manage-teachers.css'
// })
// export class ManageTeachers implements OnInit {
//   teachers: TeacherModel[] = [];

//   constructor(private teacherService: TeacherService) {}

//   ngOnInit(): void {
//     this.fetchTeachers();
//   }

//  fetchTeachers() {
//   this.teacherService.getAllTeachers().subscribe({
//     next: (res) => {
//       console.log('Teacher fetch res:', res);
//       this.teachers = res.data?.$values ?? [];
//     },
//     error: (err) => console.error('Error fetching teachers', err),
//   });
// }


//   removeTeacher() {
//     if (!confirm('Are you sure you want to deactivate your teacher account?')) return;

//     this.teacherService.deactivateTeacher().subscribe({
//       next: (_) => {
//         alert('Teacher deactivated successfully');
//         this.fetchTeachers();
//       },
//       error: (err) => console.error('Failed to deactivate teacher', err),
//     });
//   }
// }


import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeacherService } from '../../services/teacher-service';
import { TeacherModel } from '../../models/teacher-model';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  FormsModule,
  Validators
} from '@angular/forms';
import { HttpClientService } from '../../services/http-client-service';
import { confirmPasswordValidator } from '../../misc/confirm-password-validator';

@Component({
  selector: 'app-manage-teachers',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './manage-teachers.html',
  styleUrl: './manage-teachers.css'
})
export class ManageTeachers implements OnInit {
  teachers: TeacherModel[] = [];
  createTeacherForm: FormGroup;
  api = inject(HttpClientService);

  constructor(
    private teacherService: TeacherService,
    private fb: FormBuilder
  ) {
    this.createTeacherForm = this.fb.group(
      {
        name: ['', [Validators.required, Validators.minLength(3)]],
        email: ['', [Validators.required, Validators.email]],
        organization: ['', [Validators.required, Validators.minLength(3)]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', Validators.required],
        role: ['Teacher', Validators.required]
      },
      { validators: confirmPasswordValidator }
    );
  }

  ngOnInit(): void {
    this.fetchTeachers();
  }

  fetchTeachers() {
    this.teacherService.getAllTeachers().subscribe({
      next: (res) => {
        console.log('Teacher fetch res:', res);
        this.teachers = res.data?.$values ?? [];
      },
      error: (err) => console.error('Error fetching teachers', err),
    });
  }

  // removeTeacher() {
  //   if (!confirm('Are you sure you want to deactivate your teacher account?')) return;

  //   this.teacherService.deactivateTeacher().subscribe({
  //     next: (_) => {
  //       alert('Teacher deactivated successfully');
  //       this.fetchTeachers();
  //     },
  //     error: (err) => console.error('Failed to deactivate teacher', err),
  //   });
  // }
  removeTeacher(teacherId: number) {
  if (!confirm('Are you sure you want to deactivate this teacher?')) return;

  this.teacherService.deactivateTeacher(teacherId).subscribe({
    next: (_) => {
      alert('Teacher deactivated successfully');
      this.fetchTeachers();
    },
    error: (err) => console.error('Failed to deactivate teacher', err),
  });
}


  onSubmit() {
    if (this.createTeacherForm.valid) {
      this.api.post('/api/v1/Teacher', this.createTeacherForm.value).subscribe({
        next: () => {
          alert('Teacher account created successfully.');
          this.createTeacherForm.reset();
          this.createTeacherForm.get('role')?.setValue('Teacher');
          this.fetchTeachers();
        },
        error: (error) => {
          console.log(error);
          alert(error.error.errorMessage ?? 'Error creating teacher');
        },
      });
    }
  }

  // Getters for validation in HTML if needed
  get name() { return this.createTeacherForm.get('name')!; }
  get email() { return this.createTeacherForm.get('email')!; }
  get organization() { return this.createTeacherForm.get('organization')!; }
  get password() { return this.createTeacherForm.get('password')!; }
  get confirmPassword() { return this.createTeacherForm.get('confirmPassword')!; }
}
