import { Component, inject } from '@angular/core';
import { AccountManagement } from '../account-management/account-management';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { SettingsService } from '../../services/settings-service';
import { Settings } from '../../models/settings-model';

@Component({
  selector: 'app-settings-page',
  imports: [AccountManagement, ReactiveFormsModule, FormsModule],
  templateUrl: './settings-page.html',
  styleUrl: './settings-page.css',
})
export class SettingsPage {
  settings = new Settings();
  settingsService = inject(SettingsService);

  settingsForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.settingsForm = this.fb.group({
      theme: [''],
      dateFormat: [''],
      timeFormat: [''],
    });

    this.settingsService.settings$.subscribe({
      next: (settings) => {
        this.settings = settings;
        this.settingsForm.patchValue(this.settings);
      },
    });
    this.settingsService.loadSettings();
  }

  updateSettings() {
    this.settingsService.updateSettings(this.settingsForm.value);
  }
}
