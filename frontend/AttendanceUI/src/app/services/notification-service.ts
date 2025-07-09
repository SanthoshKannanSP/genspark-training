import { inject, Injectable } from '@angular/core';
import { NotificationModal } from '../models/notification-modal';
import { BehaviorSubject } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { HttpClientService } from './http-client-service';

@Injectable()
export class NotificationService {
  private hubConnection: signalR.HubConnection | null = null;
  api = inject(HttpClientService);

  messages: NotificationModal[] = [];
  messagesSubject = new BehaviorSubject<NotificationModal[]>([]);
  messages$ = this.messagesSubject.asObservable();

  constructor() {
    const username = this.api.getUsername();
    if (username != '') {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(`https://localhost:7228/notification?username=${username}`)
        .withAutomaticReconnect()
        .build();

      this.hubConnection
        .start()
        .then(() => console.log('SignalR connected'))
        .catch((err) => console.error('Connection error: ', err));

      this.hubConnection.on('AttendanceMarked', (sessionName) => {
        this.addNotification({
          message: `Attendance marked for session: ${sessionName}`,
          type: 'success',
        });
      });

      this.hubConnection.on('AttendanceUnmarked', (sessionName) =>
        this.addNotification({
          message: `Attendance unmarked for session: ${sessionName}`,
          type: 'warning',
        })
      );

      this.hubConnection.on('StudentJoined', (message) => {
        alert('Teacher Notification: ' + message);
      });
    }
  }

  stopConnection() {
    this.hubConnection?.stop();
  }

  addNotification(notification: NotificationModal) {
    this.messages.push(notification);
    this.messagesSubject.next(this.messages);
    setTimeout(() => {
      this.messages.shift();
      this.messagesSubject.next(this.messages);
    }, 5000);
  }
}
