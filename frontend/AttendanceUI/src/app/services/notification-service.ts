import { Injectable } from '@angular/core';
import { NotificationModal } from '../models/notification-modal';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class NotificationService {
  messages: NotificationModal[] = [];
  messagesSubject = new BehaviorSubject<NotificationModal[]>([]);
  messages$ = this.messagesSubject.asObservable();

  addNotification(notification: NotificationModal) {
    this.messages.push(notification);
    this.messagesSubject.next(this.messages);
    setTimeout(() => {
      this.messages.shift();
      this.messagesSubject.next(this.messages);
    }, 5000);
  }
}
