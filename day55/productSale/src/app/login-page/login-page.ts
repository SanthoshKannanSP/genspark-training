import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css',
})
export class LoginPage {
  router = inject(Router);
  loginForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      if (
        this.loginForm.get('username')?.value == 'testuser' ||
        this.loginForm.get('password')?.value == 'test123'
      ) {
        alert('Logged in successfully');
        this.router.navigateByUrl('/products');
      } else {
        alert('Invalid username or password');
      }
    }
  }
}
