import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpClientService } from '../services/http-client-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-teacher-signup-page',
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './teacher-signup-page.html',
  styleUrl: './teacher-signup-page.css',
})
export class TeacherSignupPage {
  api = inject(HttpClientService);
  router = inject(Router);
  signupForm: FormGroup;
  constructor(private fb: FormBuilder) {
    this.signupForm = this.fb.group({
      name: ['', [Validators.minLength(1)]],
      email: ['', [Validators.email]],
      organization: ['', [Validators.required]],
      password: ['', [Validators.minLength(8)]],
    });
  }

  get name() {
    return this.signupForm.get('name')!;
  }
  get email() {
    return this.signupForm.get('email')!;
  }
  get organization() {
    return this.signupForm.get('organization')!;
  }
  get password() {
    return this.signupForm.get('password')!;
  }

  onSubmit() {
    if (this.signupForm.valid) {
      console.log('Signup Form Submitted', this.signupForm.value);
      this.api.post('/api/v1/Teacher', this.signupForm.value).subscribe({
        next: () => {
          alert('Created account successfully. Now log into your account');
          this.router.navigateByUrl('/login');
        },
        error: (error) => console.log(error),
      });
    }
  }
}
