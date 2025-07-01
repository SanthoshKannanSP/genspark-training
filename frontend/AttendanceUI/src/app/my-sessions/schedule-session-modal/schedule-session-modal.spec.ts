import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleSessionModal } from './schedule-session-modal';

describe('ScheduleSessionModal', () => {
  let component: ScheduleSessionModal;
  let fixture: ComponentFixture<ScheduleSessionModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleSessionModal],
    }).compileComponents();

    fixture = TestBed.createComponent(ScheduleSessionModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
