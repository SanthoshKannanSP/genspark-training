import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleSessionPage } from './schedule-session-page';

describe('ScheduleSessionPage', () => {
  let component: ScheduleSessionPage;
  let fixture: ComponentFixture<ScheduleSessionPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScheduleSessionPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleSessionPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
