import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { User } from '../models/user';
import { addUser } from '../ngrx/user.actions';

@Component({
  selector: 'app-add-user',
  imports: [],
  templateUrl: './add-user.html',
  styleUrl: './add-user.css',
})
export class AddUser {
  constructor(private store: Store) {}
  handelAddUser() {
    const newUser = new User(102, 'Doe', 'Doe', 'user');
    this.store.dispatch(addUser({ user: newUser }));
  }
}
