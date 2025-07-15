import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingsPage } from './settings-page';
import { Component } from '@angular/core';
import { of } from 'rxjs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SettingsService } from '../../services/settings-service';
import { Settings } from '../../models/settings-model';

@Component({
  selector: 'app-account-management',
  template: '',
})
class MockAccountManagement {}

describe('SettingsPage', () => {
  let component: SettingsPage;
  let fixture: ComponentFixture<SettingsPage>;
  let mockSettingsService: jasmine.SpyObj<SettingsService>;

  const mockSettings: Settings = {
    theme: 'light',
    dateFormat: 'dd/MM/yy',
    timeFormat: 'hh:mm aa',
  };

  beforeEach(async () => {
    const settingsSpy = jasmine.createSpyObj(
      'SettingsService',
      ['loadSettings', 'updateSettings'],
      {
        settings$: of(mockSettings),
      }
    );
    await TestBed.configureTestingModule({
      imports: [SettingsPage],
      providers: [{ provide: SettingsService, useValue: settingsSpy }],
    })
      .overrideComponent(SettingsPage, {
        set: {
          imports: [MockAccountManagement, ReactiveFormsModule, FormsModule],
        },
      })
      .compileComponents();

    fixture = TestBed.createComponent(SettingsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
