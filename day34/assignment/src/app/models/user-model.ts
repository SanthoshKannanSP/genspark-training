import { AddUserModel } from './add-user-model';

export class UserModel {
  public id!: number;
  public username!: string;
  public email!: string;
  public role!: string;

  static MapAddModel(addModel: AddUserModel, uid: number): UserModel {
    return {
      id: uid,
      username: addModel.username,
      email: addModel.email,
      role: addModel.role,
    };
  }
}
