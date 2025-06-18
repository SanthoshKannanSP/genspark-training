import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AddUserModel } from '../models/add-user-model';
import { UserModel } from '../models/user-model';
import { UserService } from '../services/user-service';

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
    this.addUserForm = new FormGroup({
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      gender: new FormControl(null, Validators.required),
      role: new FormControl(null, Validators.required),
      state: new FormControl(null, Validators.required),
    });
  }
  handleSubmit() {
    let addUser: AddUserModel = {
      firstName: this.addUserForm.controls['firstName'].value,
      lastName: this.addUserForm.controls['lastName'].value,
      gender: this.addUserForm.controls['gender'].value,
      role: this.addUserForm.controls['role'].value,
      address: {
        state: this.addUserForm.controls['state'].value,
      },
    };

    this.userService.addUser(addUser).subscribe({
      next: (data) => {
        alert('User has been successfully added'!);
      },
      error: (err) => {
        alert('Unable to add user');
      },
    });
  }
}
