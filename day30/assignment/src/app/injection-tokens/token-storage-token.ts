import { InjectionToken } from '@angular/core';
import { ITokenStorage } from '../interfaces/storage-service-interface';

export const TOKEN_STORAGE_TOKEN = new InjectionToken<ITokenStorage>('TokenStorageToken');