import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpcomingSessionsContainer } from './upcoming-sessions-container';
import { Component, Input } from '@angular/core';
import { SessionModel } from '../../models/session-model';
import { SessionService } from '../../services/session-service';
import { of } from 'rxjs';

@Component({
  selector: 'app-upcoming-session-card',
  template: '',
})
class MockUpcomingSessionCard {
  @Input() session!: SessionModel;
}

@Component({
  selector: 'app-session-details',
  template: '',
})
class MockSessionDetails {
  openModal(session: SessionModel) {}
  @Input() modalId!: string;
}

describe('UpcomingSessionsContainer', () => {
  let component: UpcomingSessionsContainer;
  let fixture: ComponentFixture<UpcomingSessionsContainer>;
  let sessionServiceSpy: jasmine.SpyObj<SessionService>;

  beforeEach(async () => {
    const sessionSpy = jasmine.createSpyObj(
      'SessionService',
      ['updateUpcomingSessions'],
      {
        upcomingSessions$: of([]),
      }
    );

    await TestBed.configureTestingModule({
      imports: [UpcomingSessionsContainer],
      providers: [{ provide: SessionService, useValue: sessionSpy }],
    })
      .overrideComponent(UpcomingSessionsContainer, {
        set: {
          imports: [MockUpcomingSessionCard, MockSessionDetails],
        },
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
