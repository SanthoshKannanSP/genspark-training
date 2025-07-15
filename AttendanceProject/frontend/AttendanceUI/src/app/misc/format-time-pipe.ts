import { Pipe, PipeTransform, inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { SettingsService } from '../services/settings-service';

@Pipe({
  name: 'formatTime',
  standalone: true,
  pure: false,
})
export class FormatTimePipe implements PipeTransform {
  private datePipe = inject(DatePipe);
  private settingsService = inject(SettingsService);

  transform(value: Date | string | null | undefined): string {
    if (!value) return '';
    const format = this.settingsService.getTimeFormat();
    const date = `${new Date().toDateString()} ${value}`;
    return this.datePipe.transform(date, format) ?? '';
  }
}
