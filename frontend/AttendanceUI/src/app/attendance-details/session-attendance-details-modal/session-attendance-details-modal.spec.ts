import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionAttendanceDetailsModal } from './session-attendance-details-modal';

describe('SessionAttendanceDetailsModal', () => {
  let component: SessionAttendanceDetailsModal;
  let fixture: ComponentFixture<SessionAttendanceDetailsModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SessionAttendanceDetailsModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SessionAttendanceDetailsModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
