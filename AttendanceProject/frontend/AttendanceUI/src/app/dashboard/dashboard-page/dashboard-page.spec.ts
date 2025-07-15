import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardPage } from './dashboard-page';
import { Component } from '@angular/core';

@Component({
  selector: 'app-upcoming-sessions-container',
  template: '',
})
class MockUpcomingSessionsContainer {}

@Component({
  selector: 'app-past-sessions-container',
  template: '',
})
class MockPastSessionsContainer {}

@Component({
  selector: 'app-welcome-title',
  template: '',
})
class MockWelcomeTitle {}

describe('DashboardPage', () => {
  let component: DashboardPage;
  let fixture: ComponentFixture<DashboardPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DashboardPage],
    })
      .overrideComponent(DashboardPage, {
        set: {
          imports: [
            MockPastSessionsContainer,
            MockUpcomingSessionsContainer,
            MockWelcomeTitle,
          ],
        },
      })
      .compileComponents();

    fixture = TestBed.createComponent(DashboardPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render welcome title', () => {
    const welcomeTitle =
      fixture.nativeElement.querySelector('app-welcome-title');
    expect(welcomeTitle).toBeTruthy();
  });

  it('should render upcoming sessions container', () => {
    const upcomingSessionsContainer = fixture.nativeElement.querySelector(
      'app-upcoming-sessions-container'
    );
    expect(upcomingSessionsContainer).toBeTruthy();
  });

  it('should render past sessions container', () => {
    const pastSessionsContainer = fixture.nativeElement.querySelector(
      'app-past-sessions-container'
    );
    expect(pastSessionsContainer).toBeTruthy();
  });
});
