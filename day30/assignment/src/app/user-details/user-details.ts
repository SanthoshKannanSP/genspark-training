import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { Token } from '../models/token';
import { TOKEN_STORAGE_TOKEN } from '../injection-tokens/token-storage-token';

@Component({
  selector: 'app-user-details',
  imports: [],
  templateUrl: './user-details.html',
  styleUrl: './user-details.css'
})
export class UserDetails {
  @Input() userData!:Token;
  @Output() updateToken = new EventEmitter<Token>();
  tokenStorageService = inject(TOKEN_STORAGE_TOKEN);

  logout()
  {
    this.updateToken.emit(new Token());
    this.tokenStorageService.clearToken();
  }

}
