import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpcomingSessionsContainer } from './upcoming-sessions-container';

describe('UpcomingSessionsContainer', () => {
  let component: UpcomingSessionsContainer;
  let fixture: ComponentFixture<UpcomingSessionsContainer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpcomingSessionsContainer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpcomingSessionsContainer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
