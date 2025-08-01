import {
  ApplicationConfig,
  provideBrowserGlobalErrorListeners,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import {
  RECAPTCHA_SETTINGS,
  RECAPTCHA_V3_SITE_KEY,
  RecaptchaLoaderService,
  RecaptchaSettings,
  ReCaptchaV3Service,
} from 'ng-recaptcha';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    RecaptchaLoaderService,
    {
      provide: RECAPTCHA_SETTINGS,
      useValue: {
        siteKey: '6LelNpUrAAAAAO0D_ASbLm1jASiIvmc5iW-gmr0M',
      } as RecaptchaSettings,
    },
  ],
};
