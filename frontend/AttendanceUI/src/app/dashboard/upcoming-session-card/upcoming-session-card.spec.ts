import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpcomingSessionCard } from './upcoming-session-card';

describe('UpcomingSessionCard', () => {
  let component: UpcomingSessionCard;
  let fixture: ComponentFixture<UpcomingSessionCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpcomingSessionCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpcomingSessionCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
