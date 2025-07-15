import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveSessionBanner } from './live-session-banner';
import { LiveSessionModel } from '../models/live-session-model';
import { of } from 'rxjs';
import { LiveSessionService } from '../services/live-session-service';
import { Router } from '@angular/router';
import { By } from '@angular/platform-browser';

describe('LiveSessionBanner', () => {
  let component: LiveSessionBanner;
  let fixture: ComponentFixture<LiveSessionBanner>;
  let mockRouter: jasmine.SpyObj<Router>;

  const mockLiveSession: LiveSessionModel = {
    sessionId: 1,
    sessionName: 'Test',
    attendingStudents: [
      {
        studentId: 1,
        studentName: 'Student1',
      },
    ],
    notJoinedStudents: [
      {
        studentId: 2,
        studentName: 'Student2',
      },
    ],
  };

  beforeEach(async () => {
    const liveSessionSpy = jasmine.createSpyObj(
      'LiveSessionService',
      ['updateLiveSession'],
      {
        liveSessionDetails$: of(mockLiveSession),
      }
    );
    const routerSpy = {
      navigateByUrl: jasmine.createSpy('navigateByUrl').and.returnValue({}),
    };
    await TestBed.configureTestingModule({
      imports: [LiveSessionBanner],
      providers: [
        { provide: LiveSessionService, useValue: liveSessionSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(LiveSessionBanner);
    component = fixture.componentInstance;
    mockRouter = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to live session control page when Go To Control button clicked', () => {
    const goToControlButton = fixture.debugElement.query(
      By.css('.btn-success')
    );
    goToControlButton.triggerEventHandler('click', null);

    expect(mockRouter.navigateByUrl).toHaveBeenCalledWith('/session/live');
  });
});
