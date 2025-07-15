import {
  ApplicationConfig,
  provideBrowserGlobalErrorListeners,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { HttpClientService } from './services/http-client-service';
import { SessionService } from './services/session-service';
import { NotificationService } from './services/notification-service';
import { AttendanceService } from './services/attendance-service';
import { SettingsService } from './services/settings-service';
import { DatePipe } from '@angular/common';
import { AccountService } from './services/account-service';
import { LiveSessionService } from './services/live-session-service';
import { NoteService } from './services/note-service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    HttpClientService,
    SessionService,
    NotificationService,
    AttendanceService,
    DatePipe,
    SettingsService,
    AccountService,
    LiveSessionService,
    NoteService,
  ],
};
