import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SessionDetails } from './session-details';
import { HttpClientService } from '../../services/http-client-service';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { FormatTimePipe } from '../../misc/format-time-pipe';
import { SessionModel, TeacherDetails } from '../../models/session-model';
import { DatePipe } from '@angular/common';
import { SettingsService } from '../../services/settings-service';

describe('SessionDetailsComponent', () => {
  let component: SessionDetails;
  let fixture: ComponentFixture<SessionDetails>;
  let httpClientService: jasmine.SpyObj<HttpClientService>;

  const session: SessionModel = {
    sessionId: 1,
    sessionName: 'Test Session',
    date: '2023-10-10',
    startTime: '10:00',
    endTime: '12:00',
    attended: true,
    teacherDetails: new TeacherDetails(),
    sessionLink: 'abc',
    sessionCode: 'abc',
    status: 'Completed',
  };

  beforeEach(async () => {
    const httpClientSpy = {
      hasRole: jasmine
        .createSpy('hasRole')
        .and.callFake((role) => role == 'Student'),
    };
    const settingsSpy = {
      getTimeFormat: jasmine
        .createSpy('getTimeFormat')
        .and.returnValue('hh:mm aa'),
      getDateFormat: jasmine
        .createSpy('getDateFormat')
        .and.returnValue('dd/MM/yyyy'),
    };

    await TestBed.configureTestingModule({
      imports: [SessionDetails, FormatDatePipe, FormatTimePipe],
      providers: [
        { provide: HttpClientService, useValue: httpClientSpy },
        { provide: SettingsService, useValue: settingsSpy },
        { provide: DatePipe },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(SessionDetails);
    component = fixture.componentInstance;
    httpClientService = TestBed.inject(
      HttpClientService
    ) as jasmine.SpyObj<HttpClientService>;

    component.modalId = 'testModal';
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set student to true if role is Student', () => {
    expect(component.student).toBeTrue();
    expect(httpClientService.hasRole).toHaveBeenCalledWith('Student');
  });

  it('should open modal with session details', () => {
    component.openModal(session);
    expect(component.session).toEqual(session);
  });

  it('should render session details in the template when session is set', () => {
    component.openModal(session);
    fixture.detectChanges();

    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('.modal-title').textContent).toContain(
      'Session Details'
    );
    expect(
      compiled.querySelectorAll('.modal-body div')[0].textContent
    ).toContain('Session Name:');
    expect(
      compiled.querySelectorAll('.modal-body div')[1].textContent
    ).toContain('Test Session');
  });
});
