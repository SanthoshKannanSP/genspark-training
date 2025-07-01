import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClientService } from '../services/http-client-service';
import { dateOfBirthValidator } from '../misc/date-of-birth-validator';

@Component({
  selector: 'app-student-signup-page',
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './student-signup-page.html',
  styleUrl: './student-signup-page.css',
})
export class StudentSignupPage {
  api = inject(HttpClientService);
  router = inject(Router);
  signupForm: FormGroup;

  genders = ['Male', 'Female'];

  constructor(private fb: FormBuilder) {
    this.signupForm = this.fb.group({
      name: ['', [Validators.minLength(1)]],
      gender: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required, dateOfBirthValidator]],
      email: ['', [Validators.email]],
      password: ['', [Validators.minLength(8)]],
    });
  }

  onSubmit() {
    if (this.signupForm.valid) {
      console.log('Form submitted:', this.signupForm.value);
      this.api.post('/api/v1/Student', this.signupForm.value).subscribe({
        next: () => {
          alert('Created account successfully. Now log into your account');
          this.router.navigateByUrl('/login');
        },
        error: (error) => console.log(error),
      });
    }
  }

  get name() {
    return this.signupForm.get('name')!;
  }

  get gender() {
    return this.signupForm.get('gender')!;
  }

  get dateOfBirth() {
    return this.signupForm.get('dateOfBirth');
  }

  get email() {
    return this.signupForm.get('email')!;
  }

  get password() {
    return this.signupForm.get('password')!;
  }
}
