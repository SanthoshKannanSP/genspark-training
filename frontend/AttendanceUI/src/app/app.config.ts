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
  ],
};
