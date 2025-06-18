export class UserModel {
  public id!: number;
  public firstName!: string;
  public lastName!: string;
  public gender!: string;
  public role!: string;
  public state!: string;

  static MapResponse(response: any): UserModel {
    return {
      id: response.id,
      firstName: response.firstName,
      lastName: response.lastName,
      gender: response.gender,
      role: response.role,
      state: response.address.state,
    };
  }
}
