import { Injectable } from '@angular/core';

@Injectable()
export class SettingsService {
  private currentFormat: string = 'dd/MM/yyyy';

  setDateFormat(format: string) {
    this.currentFormat = format;
  }

  getDateFormat(): string {
    return this.currentFormat;
  }
}
