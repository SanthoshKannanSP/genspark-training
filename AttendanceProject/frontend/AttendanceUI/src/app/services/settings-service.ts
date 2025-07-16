import { inject, Injectable } from '@angular/core';
import { Settings } from '../models/settings-model';
import { BehaviorSubject } from 'rxjs';
import { HttpClientService } from './http-client-service';
import { NotificationService } from './notification-service';

@Injectable()
export class SettingsService {
  settings = new BehaviorSubject<Settings>(new Settings());
  settings$ = this.settings.asObservable();
  api = inject(HttpClientService);
  notificationService = inject(NotificationService);

  constructor() {
    this.api.token$.subscribe({
      next: (token: any) => {
        if (token != null && token != '') {
          this.loadSettings();
        }
      },
    });
  }

  loadSettings() {
    this.api.get('/api/v1/Settings', true).subscribe({
      next: (data: any) => {
        this.settings.next(data.data);
      },
      error: (error) => console.log(error),
    });
  }

  updateSettings(settings: Settings) {
    this.api.post('/api/v1/Settings/Update', settings, true).subscribe({
      next: (data: any) => {
        this.settings.next(data.data);
        this.notificationService.addNotification({
          message: 'Settings updated successfully',
          type: 'success',
        });
      },
      error: (error) => {
        console.log(error);
        this.notificationService.addNotification({
          message: 'Unable to update notifications. Please try again later',
          type: 'success',
        });
      },
    });
  }

  getDateFormat(): string {
    return this.settings.value.dateFormat;
  }

  getTimeFormat(): string {
    return this.settings.value.timeFormat;
  }

  getTheme(): string {
    return this.settings.value.theme;
  }
}
