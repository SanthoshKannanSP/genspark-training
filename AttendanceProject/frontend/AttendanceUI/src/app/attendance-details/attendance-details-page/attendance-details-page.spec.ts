import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AttendanceDetailsPage } from './attendance-details-page';
import { Component, NO_ERRORS_SCHEMA } from '@angular/core';

@Component({
  selector: 'app-filter-menu',
  template: '',
})
class MockFilterMenu {}

@Component({
  selector: 'app-attendance-table',
  template: '',
})
class MockAttendanceTable {}

describe('AttendanceDetailsPage', () => {
  let component: AttendanceDetailsPage;
  let fixture: ComponentFixture<AttendanceDetailsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AttendanceDetailsPage],
    })
      .overrideComponent(AttendanceDetailsPage, {
        set: {
          imports: [MockFilterMenu, MockAttendanceTable],
        },
      })
      .compileComponents();

    fixture = TestBed.createComponent(AttendanceDetailsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should render title', () => {
    const header = fixture.nativeElement.querySelector('h1');
    expect(header?.textContent).toContain('Attendance Details');
  });

  it('should render filter menu', () => {
    const filterMenu = fixture.nativeElement.querySelector('app-filter-menu');
    expect(filterMenu).toBeTruthy();
  });

  it('should render attendance table', () => {
    const table = fixture.nativeElement.querySelector('app-attendance-table');
    expect(table).toBeTruthy();
  });
});
