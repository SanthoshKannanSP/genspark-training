import { BehaviorSubject } from 'rxjs';
import { AddUserModel } from '../models/add-user-model';
import { AuthenticationModel } from '../models/authentication-model';

export class AuthenticationService {
  authentications = new BehaviorSubject<AuthenticationModel[]>([]);
  authentications$ = this.authentications.asObservable();
  addAuthentication(addUser: AddUserModel) {
    let newAuth = AuthenticationModel.MapAddModel(addUser);
    this.authentications.next([...this.authentications.value, newAuth]);
  }
}
