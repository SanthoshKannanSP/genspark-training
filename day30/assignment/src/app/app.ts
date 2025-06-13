import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginForm } from "./login-form/login-form";
import { Token } from './models/token';
import { UserDetails } from "./user-details/user-details";
import { TOKEN_STORAGE_TOKEN } from './injection-tokens/token-storage-token';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoginForm, UserDetails],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'assignment';
  tokenStorageService = inject(TOKEN_STORAGE_TOKEN);
  token:Token = this.tokenStorageService.getToken();

  handleToken(value:Token)
  {
    this.token = value;
  }
}
