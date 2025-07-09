import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PastSessionsContainer } from './past-sessions-container';
import { SessionService } from '../../services/session-service';
import { SessionModel, TeacherDetails } from '../../models/session-model';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-past-session-card',
  template: '',
})
class MockPastSessionCard {
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

@Component({
  selector: 'app-session-attendance-details-modal',
  template: '',
})
class MockSessionAttendanceDetailsModal {}

describe('PastSessionsContainer', () => {
  let component: PastSessionsContainer;
  let fixture: ComponentFixture<PastSessionsContainer>;
  let sessionServiceSpy: jasmine.SpyObj<SessionService>;

  const mockSessions: SessionModel[] = [
    {
      sessionName: 'Session 1',
      date: '2023-10-10',
      startTime: '10:00',
      endTime: '12:00',
      attended: true,
      sessionCode: 'abc',
      sessionLink: 'abc',
      sessionId: 1,
      status: 'Completed',
      teacherDetails: new TeacherDetails(),
    },
    {
      sessionName: 'Session 2',
      date: '2023-10-11',
      startTime: '11:00',
      endTime: '13:00',
      attended: false,
      sessionCode: 'abc',
      sessionLink: 'abc',
      sessionId: 1,
      status: 'Completed',
      teacherDetails: new TeacherDetails(),
    },
  ];

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('SessionService', ['updatePastSessions'], {
      pastSessions$: of([]),
    });

    await TestBed.configureTestingModule({
      imports: [PastSessionsContainer],
      providers: [{ provide: SessionService, useValue: spy }],
    })
      .overrideComponent(PastSessionsContainer, {
        set: {
          imports: [
            MockPastSessionCard,
            MockSessionDetails,
            MockSessionAttendanceDetailsModal,
          ],
        },
      })
      .compileComponents();

    sessionServiceSpy = TestBed.inject(
      SessionService
    ) as jasmine.SpyObj<SessionService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PastSessionsContainer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display "No sessions found" when pastSessions is empty', () => {
    const noSessionsMessage = fixture.debugElement.query(
      By.css('.text-center')
    ).nativeElement;
    expect(noSessionsMessage.textContent).toContain('No sessions found');
  });

  it('should render PastSessionCard components when pastSessions is not empty', () => {
    sessionServiceSpy.pastSessions$ = of(mockSessions);
    component.pastSessions = mockSessions;
    fixture.detectChanges();

    const sessionCards = fixture.debugElement.queryAll(
      By.css('app-past-session-card')
    );
    expect(sessionCards.length).toBe(2);
  });

  it('should call viewSession method when a session is clicked', () => {
    spyOn(component, 'viewSession');

    sessionServiceSpy.pastSessions$ = of(mockSessions);
    component.pastSessions = mockSessions;
    fixture.detectChanges();

    const sessionCard = fixture.debugElement.query(
      By.css('app-past-session-card')
    );
    sessionCard.triggerEventHandler('viewSession', mockSessions[0]);
    expect(component.viewSession).toHaveBeenCalledWith(mockSessions[0]);
  });

  it('should open modal when viewSession is called', () => {
    spyOn(component.sessionDetailsModal, 'openModal');

    component.viewSession(mockSessions[0]);

    expect(component.sessionDetailsModal.openModal).toHaveBeenCalledWith(
      mockSessions[0]
    );
  });
});
