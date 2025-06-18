export class AddUserModel {
  public firstName!: string;
  public lastName!: string;
  public gender!: string;
  public role!: string;
  public address!: Address;
}

class Address {
  public state!: string;
}
