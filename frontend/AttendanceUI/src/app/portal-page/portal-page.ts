import { Component } from '@angular/core';
import { Sidebar } from '../sidebar/sidebar';
import { RouterOutlet } from '@angular/router';
import { NotificationToast } from '../notification-toast/notification-toast';

@Component({
  selector: 'app-portal-page',
  imports: [Sidebar, RouterOutlet, NotificationToast],
  templateUrl: './portal-page.html',
  styleUrl: './portal-page.css',
})
export class PortalPage {}
