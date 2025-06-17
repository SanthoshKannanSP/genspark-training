import { BehaviorSubject, Observable } from 'rxjs';
import { LoginModel } from '../models/login-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class LoginService {
  private http = inject(HttpClient);
  private route = inject(Router);
  private usernameSubject = new BehaviorSubject<string | null>(
    localStorage.getItem('token') ?? null
  );
  username$: Observable<string | null> = this.usernameSubject.asObservable();

  validateUserLogin(user: LoginModel) {
    if (user.username.length < 3) {
      this.usernameSubject.next(null);
    } else {
      this.callLoginAPI(user).subscribe({
        next: (data: any) => {
          this.usernameSubject.next(user.username);
          localStorage.setItem('token', data.accessToken);
          this.route.navigateByUrl('/profile');
        },
        error: () => {
          alert('Invalid username or password');
        },
      });
    }
  }

  callLoginAPI(user: LoginModel) {
    return this.http.post('https://dummyjson.com/auth/login', user);
  }
  logout() {
    this.usernameSubject.next(null);
    localStorage.removeItem('token');
    this.route.navigateByUrl('/login');
  }
}
