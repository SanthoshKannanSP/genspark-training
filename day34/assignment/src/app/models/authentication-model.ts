import { AddUserModel } from './add-user-model';

export class AuthenticationModel {
  username!: string;
  password!: string;

  static MapAddModel(addUser: AddUserModel): AuthenticationModel {
    return {
      username: addUser.username,
      password: addUser.password,
    };
  }
}
