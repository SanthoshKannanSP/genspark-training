import { Component, EventEmitter, inject, Output, output } from '@angular/core';
import { AuthenticationService } from '../services/authentication-service';
import { FormsModule } from '@angular/forms';
import { JsonPipe } from '@angular/common';
import { Token } from '../models/token';
import { TOKEN_STORAGE_TOKEN } from '../injection-tokens/token-storage-token';

@Component({
  selector: 'app-login-form',
  imports: [FormsModule],
  templateUrl: './login-form.html',
  styleUrl: './login-form.css'
})
export class LoginForm {
  @Output() updateToken = new EventEmitter<Token>();
  authenticationService = inject(AuthenticationService);
  tokenStorageService = inject(TOKEN_STORAGE_TOKEN);
  username:string = "";
  password:string = "";

  login()
  {
    let token = this.authenticationService.ValidateUser({username: this.username, password: this.password})
    
    if (token)
    {
      this.tokenStorageService.setToken(token)
      this.updateToken.emit(token);
    }
    else
      alert("Invalid username or password");
  }
}
