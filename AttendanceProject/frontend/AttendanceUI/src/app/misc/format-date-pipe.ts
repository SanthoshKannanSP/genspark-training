import { Pipe, PipeTransform, inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { SettingsService } from '../services/settings-service';

@Pipe({
  name: 'formatDate',
  standalone: true,
  pure: false,
})
export class FormatDatePipe implements PipeTransform {
  private datePipe = inject(DatePipe);
  private settingsService = inject(SettingsService);

  transform(value: Date | string | null | undefined): string {
    if (!value) return '';
    const format = this.settingsService.getDateFormat();
    return this.datePipe.transform(value, format) ?? '';
  }
}
