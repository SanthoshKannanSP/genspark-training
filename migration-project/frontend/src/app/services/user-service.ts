import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = 'http://localhost:5288/api/User';
  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get<User[]>(`${this.baseUrl}`);
  }
}
