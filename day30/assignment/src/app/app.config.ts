import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { AuthenticationService } from './services/authentication-service';
import { TOKEN_STORAGE_TOKEN } from './injection-tokens/token-storage-token';
import { SessionStorageService } from './services/session-storage-service';
import { LocalStorageService } from './services/local-storage-service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    AuthenticationService,
    {provide: TOKEN_STORAGE_TOKEN, useClass: LocalStorageService}
  ]
};
