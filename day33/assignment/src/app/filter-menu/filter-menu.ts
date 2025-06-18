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
      gender: new FormControl(null, Validators.required),
      role: new FormControl(null, Validators.required),
    });
  }

  handleSubmit() {
    const gender = this.filterForm.controls['gender'].value;
    const role = this.filterForm.controls['role'].value;
    this.userService.getFilteredUsers(role, gender, '');
  }
}
