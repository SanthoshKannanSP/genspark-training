import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceTable } from './attendance-table';
import { Component, Input } from '@angular/core';
import { AttendanceService } from '../../services/attendance-service';
import { PaginatedResponse } from '../../models/paginated-response';
import { AttendanceDetailsModel } from '../../models/attendance-details-modal';
import { of } from 'rxjs';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { FormatTimePipe } from '../../misc/format-time-pipe';
import { DatePipe } from '@angular/common';
import { SettingsService } from '../../services/settings-service';

@Component({
  selector: 'app-session-attendance-details-modal',
  standalone: true,
  template: '',
})
class MockSessionAttendanceDetailsModal {
  @Input() modalId!: string;
  openModal = jasmine.createSpy('openModal');
}

describe('AttendanceTable', () => {
  let component: AttendanceTable;
  let fixture: ComponentFixture<AttendanceTable>;

  let attendanceService: jasmine.SpyObj<AttendanceService>;
  let settingsService: jasmine.SpyObj<SettingsService>;

  const mockData: PaginatedResponse<AttendanceDetailsModel[]> = {
    data: [
      {
        sessionId: 1,
        sessionName: 'Test Session',
        date: '2025-07-05',
        startTime: '09:00',
        endTime: '10:00',
        attendedCount: 8,
        registeredCount: 10,
        status: 'Completed',
      },
    ],
    pagination: {
      page: 1,
      pageSize: 1,
      totalPages: 2,
      totalRecords: 2,
    },
  };

  beforeEach(async () => {
    attendanceService = jasmine.createSpyObj(
      'AttendanceService',
      ['updateAllSessions'],
      {
        attendanceDetails$: of(mockData),
      }
    );

    settingsService = jasmine.createSpyObj('SettingsService', [
      'getDateFormat',
      'getTimeFormat',
    ]);

    await TestBed.configureTestingModule({
      imports: [AttendanceTable],
      providers: [
        { provide: AttendanceService, useValue: attendanceService },
        { provide: SettingsService, useValue: settingsService },
        { provide: DatePipe },
      ],
    })
      .overrideComponent(AttendanceTable, {
        set: {
          imports: [
            MockSessionAttendanceDetailsModal,
            FormatDatePipe,
            FormatTimePipe,
          ],
        },
      })
      .compileComponents();

    fixture = TestBed.createComponent(AttendanceTable);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
