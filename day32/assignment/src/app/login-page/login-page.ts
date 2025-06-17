import { Component } from '@angular/core';
import { LoginModel } from '../models/login-model';
import { LoginService } from '../services/login-service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [FormsModule],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css',
})
export class LoginPage {
  user: LoginModel = new LoginModel();
  constructor(private userService: LoginService, private route: Router) {}
  handleLogin() {
    this.userService.validateUserLogin(this.user);
  }
}
