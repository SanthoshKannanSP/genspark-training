import { inject, Injectable } from '@angular/core';
import { AddUserModel } from '../models/add-user-model';
import { UserModel } from '../models/user-model';
import { BehaviorSubject } from 'rxjs';
import { AuthenticationService } from './authentication-service';
import { sampleData } from '../misc/sample-data';

@Injectable()
export class UserService {
  static id = 21;
  authenticationService = inject(AuthenticationService);
  data: UserModel[] = sampleData;
  users = new BehaviorSubject<UserModel[]>(sampleData);
  users$ = this.users.asObservable();

  addUser(user: AddUserModel) {
    let newUser = UserModel.MapAddModel(user, UserService.id++);
    this.data = this.data.concat(newUser);
    this.users.next(this.data);
    console.log(this.users.value);
  }

  filterUsers(username: string, role: string) {
    let filteredUsers = this.data;
    filteredUsers =
      username == null || username.length == 0
        ? filteredUsers
        : filteredUsers.filter((user) => user.username.includes(username));
    console.log(filteredUsers);

    filteredUsers =
      role == null || role.length == 0
        ? filteredUsers
        : filteredUsers.filter((user) => user.role == role);
    this.users.next(filteredUsers);
  }
}
