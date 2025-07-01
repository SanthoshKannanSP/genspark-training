import { Component, inject } from '@angular/core';
import { NotificationService } from '../services/notification-service';
import { NotificationModal } from '../models/notification-modal';

@Component({
  selector: 'app-notification-toast',
  imports: [],
  templateUrl: './notification-toast.html',
  styleUrl: './notification-toast.css',
})
export class NotificationToast {
  notificationService = inject(NotificationService);
  notifications: NotificationModal[] = [];

  constructor() {
    this.notificationService.messages$.subscribe({
      next: (data) => {
        this.notifications = data;
      },
      error: (error) => console.log(error),
    });
  }
}
