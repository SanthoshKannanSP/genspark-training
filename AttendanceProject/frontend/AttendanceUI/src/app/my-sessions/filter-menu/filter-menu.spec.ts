import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterMenu } from './filter-menu';
import { SessionService } from '../../services/session-service';
import { AttendanceService } from '../../services/attendance-service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { FilterModel } from '../../models/filter-model';

describe('FilterMenu', () => {
  let component: FilterMenu;
  let fixture: ComponentFixture<FilterMenu>;
  let mockRouter: jasmine.SpyObj<Router>;
  let mockSessionService: jasmine.SpyObj<SessionService>;
  let mockAttendanceService: jasmine.SpyObj<AttendanceService>;

  const mockFilter = {
    sessionName: 'Test',
    status: 'Completed',
    startDate: '2025-01-02',
    endDate: '2025-02-03',
    startTime: '10:00',
    endTime: '11:00',
  };

  const mockEmptyFilter = {
    sessionName: '',
    status: '',
    startDate: '',
    endDate: '',
    startTime: '',
    endTime: '',
  };

  beforeEach(async () => {
    const sessionSpy = jasmine.createSpyObj('SessionService', [
      'updateAllSessions',
    ]);
    const attendanceSpy = jasmine.createSpyObj('AttendanceService', [
      'updateAllSessions',
    ]);
    const routerSpy = jasmine.createSpyObj('Router', [], {
      url: '/portal/sessions',
    });
    await TestBed.configureTestingModule({
      imports: [FilterMenu],
      providers: [
        { provide: SessionService, useValue: sessionSpy },
        { provide: AttendanceService, useValue: attendanceSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(FilterMenu);
    component = fixture.componentInstance;

    mockSessionService = TestBed.inject(
      SessionService
    ) as jasmine.SpyObj<SessionService>;
    mockAttendanceService = TestBed.inject(
      AttendanceService
    ) as jasmine.SpyObj<AttendanceService>;
    mockRouter = TestBed.inject(Router) as jasmine.SpyObj<Router>;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should reset filter form in reset', () => {
    component.filterForm.patchValue(mockFilter);

    spyOn(component, 'applyFilters');
    spyOn(component.filterForm, 'reset');
    component.resetFilters();

    expect(component.filterForm.reset).toHaveBeenCalled();
    expect(component.applyFilters).toHaveBeenCalled();
  });

  it('should apply filters and update teacher sessions', () => {
    component.filterForm.patchValue(mockFilter);
    mockSessionService.updateAllSessions.and.returnValue();

    component.applyFilters();

    expect(mockSessionService.updateAllSessions).toHaveBeenCalled();
  });
});
