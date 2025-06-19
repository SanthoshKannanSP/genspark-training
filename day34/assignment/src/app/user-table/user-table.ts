import { Component, inject } from '@angular/core';
import { UserService } from '../services/user-service';
import { UserModel } from '../models/user-model';

@Component({
  selector: 'app-user-table',
  imports: [],
  templateUrl: './user-table.html',
  styleUrl: './user-table.css',
})
export class UserTable {
  userService = inject(UserService);
  users: UserModel[] = [];

  constructor() {
    this.userService.users$.subscribe({
      next: (data) => (this.users = data),
    });
  }
}
