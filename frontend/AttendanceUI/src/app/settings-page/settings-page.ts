import { Component, inject } from '@angular/core';
import { SettingsService } from '../services/settings-service';
import { AccountManagement } from './account-management/account-management';

@Component({
  selector: 'app-settings-page',
  imports: [AccountManagement],
  templateUrl: './settings-page.html',
  styleUrl: './settings-page.css',
})
export class SettingsPage {
  settings = inject(SettingsService);
  onChange(event: Event) {
    const format = (event.target as HTMLSelectElement).value;
    this.settings.setDateFormat(format);
  }
}
