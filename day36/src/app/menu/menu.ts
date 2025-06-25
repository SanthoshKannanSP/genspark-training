import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginService } from '../services/login-service';

@Component({
  selector: 'app-menu',
  imports: [RouterLink],
  templateUrl: './menu.html',
  styleUrl: './menu.css',
})
export class Menu {
  userService = inject(LoginService);
  username: string | null = null;

  constructor() {
    this.userService.username$.subscribe({
      next: (data) => (this.username = data),
    });
  }
}
