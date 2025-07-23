import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceEditRequests } from './attendance-edit-requests';

describe('AttendanceEditRequests', () => {
  let component: AttendanceEditRequests;
  let fixture: ComponentFixture<AttendanceEditRequests>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AttendanceEditRequests]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttendanceEditRequests);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
