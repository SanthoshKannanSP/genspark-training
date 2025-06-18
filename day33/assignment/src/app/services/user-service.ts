import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AddUserModel } from '../models/add-user-model';
import { UserModel } from '../models/user-model';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class UserService {
  http = inject(HttpClient);
  users = new BehaviorSubject<UserModel[]>([]);
  users$ = this.users.asObservable();

  getAllUsers() {
    return this.http.get('https://dummyjson.com/users');
  }

  addUser(user: AddUserModel) {
    return this.http.post('https://dummyjson.com/users/add', user);
  }

  filterByRole(users: UserModel[], selectedRole: string) {
    if (selectedRole == null || selectedRole.length == 0) return users;

    return users.filter((user) => user.role == selectedRole);
  }

  filterByGender(users: UserModel[], selectedGender: string) {
    if (selectedGender == null || selectedGender.length == 0) return users;

    return users.filter((user) => user.gender == selectedGender);
  }

  filterByState(users: UserModel[], selectedState: string) {
    if (selectedState == null || selectedState.length == 0) return users;

    return users.filter((user) => user.state == selectedState);
  }

  getFilteredUsers(role: string, gender: string, state: string) {
    this.getAllUsers().subscribe({
      next: (data: any) => {
        let users = data.users.map((item: any) => UserModel.MapResponse(item));
        users = this.filterByGender(users, gender);
        users = this.filterByRole(users, role);
        users = this.filterByState(users, state);
        this.users.next(users);
      },
    });
  }
}
