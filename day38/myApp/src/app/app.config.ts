import {
  ApplicationConfig,
  provideBrowserGlobalErrorListeners,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { ProductService } from './services/product-service';
import { LoginService } from './services/login-service';
import { provideState, provideStore } from '@ngrx/store';
import { userReducer } from './ngrx/user.reducer';
import { IMAGE_LOADER, ImageLoaderConfig } from '@angular/common';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    ProductService,
    LoginService,
    provideStore(),
    provideState('user', userReducer),
    {
      provide: IMAGE_LOADER,
      useValue: (config: ImageLoaderConfig) => {
        return `https://api.dicebear.com/9.x/adventurer/png?seed=${config.src}&size=${config.loaderParams?.['size']}`;
      },
    },
  ],
};
