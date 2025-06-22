import { Component, inject } from '@angular/core';
import { AuthenticationService } from '../services/authentication-service';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css',
})
export class LoginPage {
  authenticationService = inject(AuthenticationService);
  router = inject(Router);
  loginForm: FormGroup;

  constructor() {
    this.loginForm = new FormGroup({
      username: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [
        Validators.required,
        Validators.minLength(8),
      ]),
    });
  }

  onSubmit() {
    if (this.loginForm.valid)
      this.authenticationService.login(this.loginForm.value).subscribe({
        next: (data) => {
          alert('Successfully logged in. Redirecting...');
          this.router.navigateByUrl('portal/dashboard');
        },
        error: (error) => {
          alert('Error: Invalid email or password');
          console.log(error);
        },
      });
  }
}
