import { Component, inject } from '@angular/core';
import { HttpClientService } from '../../services/http-client-service';
import { AccountService } from '../../services/account-service';

@Component({
  selector: 'app-welcome-title',
  imports: [],
  templateUrl: './welcome-title.html',
  styleUrl: './welcome-title.css',
})
export class WelcomeTitle {
  accountService = inject(AccountService);
  name = '';

  constructor() {
    this.accountService.account$.subscribe({
      next: (data) => (this.name = data.name),
      error: (error) => console.log(error),
    });
  }
}
