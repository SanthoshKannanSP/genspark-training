import { Component, inject } from '@angular/core';
import { Sidebar } from '../sidebar/sidebar';
import { RouterOutlet } from '@angular/router';
import { NotificationToast } from '../notification-toast/notification-toast';
import { LiveSessionBanner } from '../live-session-banner/live-session-banner';
import { AccountService } from '../services/account-service';

@Component({
  selector: 'app-portal-page',
  imports: [Sidebar, RouterOutlet, NotificationToast, LiveSessionBanner],
  templateUrl: './portal-page.html',
  styleUrl: './portal-page.css',
})
export class PortalPage {
  accountService = inject(AccountService);

  constructor() {
    this.accountService.getAccountDetails();
  }
}
