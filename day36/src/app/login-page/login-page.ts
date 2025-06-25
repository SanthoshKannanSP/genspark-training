import { Component } from '@angular/core';
import { LoginModel } from '../models/login-model';
import { LoginService } from '../services/login-service';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { UserModel } from '../models/user-model';
import { textValidator } from '../miscs/text-validator';

@Component({
  selector: 'app-login-page',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css',
})
export class LoginPage {
  user: LoginModel = new LoginModel();
  loginForm: FormGroup;
  constructor(private userService: LoginService, private route: Router) {
    this.loginForm = new FormGroup({
      un: new FormControl(null, Validators.required),
      pass: new FormControl(null, [Validators.required, textValidator()]),
    });
  }

  public get un(): any {
    return this.loginForm.get('un');
  }
  public get pass(): any {
    return this.loginForm.get('pass');
  }
  handleLogin() {
    if (this.loginForm.invalid) return;
    this.user = new LoginModel(this.un.value, this.pass.value);
    this.userService.validateUserLogin(this.user);
  }
}
