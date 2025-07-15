import { inject, Injectable } from '@angular/core';
import { Settings } from '../models/settings-model';
import { BehaviorSubject } from 'rxjs';
import { HttpClientService } from './http-client-service';

@Injectable()
export class SettingsService {
  settings = new BehaviorSubject<Settings>(new Settings());
  settings$ = this.settings.asObservable();
  api = inject(HttpClientService);

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
      },
      error: (error) => {
        console.log(error);
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
