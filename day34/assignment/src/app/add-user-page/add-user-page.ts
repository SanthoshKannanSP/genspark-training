import { Component, inject } from '@angular/core';
import {
  EmailValidator,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AddUserModel } from '../models/add-user-model';
import { UserService } from '../services/user-service';
import { usernameValidator } from '../misc/username-validator';
import { passwordValidator } from '../misc/password-validator';
import { confirmPasswordValidator } from '../misc/confirm-password-validator';

@Component({
  selector: 'app-add-user-page',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './add-user-page.html',
  styleUrl: './add-user-page.css',
})
export class AddUserPage {
  addUserForm: FormGroup;
  userService = inject(UserService);

  constructor() {
    this.addUserForm = new FormGroup(
      {
        username: new FormControl(null, [
          Validators.required,
          usernameValidator(),
        ]),
        email: new FormControl(null, [Validators.required, Validators.email]),
        role: new FormControl(null, [Validators.required]),
        password: new FormControl(null, [
          Validators.required,
          Validators.minLength(8),
          passwordValidator(),
        ]),
        confirmPassword: new FormControl(null, Validators.required),
      },
      { validators: confirmPasswordValidator() }
    );
  }

  public get username(): any {
    return this.addUserForm.get('username');
  }

  public get email(): any {
    return this.addUserForm.get('email');
  }
  public get password(): any {
    return this.addUserForm.get('password');
  }
  public get confirmPassword(): any {
    return this.addUserForm.get('confirmPassword');
  }
  handleSubmit() {
    let addUser: AddUserModel = {
      username: this.addUserForm.controls['username'].value,
      email: this.addUserForm.controls['email'].value,
      role: this.addUserForm.controls['role'].value,
      password: this.addUserForm.controls['password'].value,
      confirmPassword: this.addUserForm.controls['confirmPassword'].value,
    };
    this.userService.addUser(addUser);
    alert('User added successfully');
  }
}
