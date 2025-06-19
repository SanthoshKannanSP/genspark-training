import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { UserService } from '../services/user-service';

@Component({
  selector: 'app-filter-menu',
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './filter-menu.html',
  styleUrl: './filter-menu.css',
})
export class FilterMenu {
  filterForm!: FormGroup;
  userService = inject(UserService);

  constructor() {
    this.filterForm = new FormGroup({
      username: new FormControl(null),
      role: new FormControl(null),
    });
  }

  handleSubmit() {
    const username = this.filterForm.controls['username'].value;
    const role = this.filterForm.controls['role'].value;
    console.log(username + ' ' + role);

    this.userService.filterUsers(username, role);
  }
}
