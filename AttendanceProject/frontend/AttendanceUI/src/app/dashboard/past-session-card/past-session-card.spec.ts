import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PastSessionCard } from './past-session-card';
import { HttpClientService } from '../../services/http-client-service';
import { SessionModel, TeacherDetails } from '../../models/session-model';
import { FormatDatePipe } from '../../misc/format-date-pipe';
import { FormatTimePipe } from '../../misc/format-time-pipe';
import { By } from '@angular/platform-browser';
import { DatePipe } from '@angular/common';
import { SettingsService } from '../../services/settings-service';
import { of } from 'rxjs';
import { AttendanceService } from '../../services/attendance-service';

describe('PastSessionCard', () => {
  let component: PastSessionCard;
  let fixture: ComponentFixture<PastSessionCard>;
  let httpClientServiceSpy: jasmine.SpyObj<HttpClientService>;

  beforeEach(async () => {
    const httpSpy = {
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

    const fakePdfBlob = new Blob([new Uint8Array([0x25, 0x50, 0x44, 0x46])], {
      type: 'application/pdf',
    });

    const attendanceSpy = {
      generateAttendanceReport: jasmine
        .createSpy('generateAttendanceReport')
        .and.returnValue(of(fakePdfBlob)),
    };

    await TestBed.configureTestingModule({
      imports: [PastSessionCard, FormatDatePipe, FormatTimePipe],
      providers: [
        { provide: HttpClientService, useValue: httpSpy },
        { provide: SettingsService, useValue: settingsSpy },
        { provide: AttendanceService, useValue: attendanceSpy },
        { provide: DatePipe },
      ],
    }).compileComponents();

    httpClientServiceSpy = TestBed.inject(
      HttpClientService
    ) as jasmine.SpyObj<HttpClientService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PastSessionCard);
    component = fixture.componentInstance;
    component.session = {
      sessionId: 1,
      sessionName: 'Test Session',
      date: '2023-10-10',
      startTime: '10:00',
      endTime: '12:00',
      attended: true,
      sessionCode: 'abc',
      sessionLink: 'abc',
      teacherDetails: new TeacherDetails(),
      status: 'Completed',
    };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should emit viewSession event when showDetails is called', () => {
    spyOn(component.viewSession, 'emit');
    component.showDetails();
    expect(component.viewSession.emit).toHaveBeenCalledWith(component.session);
  });

  it('should display session details correctly', () => {
    const sessionNameElement = fixture.debugElement.query(
      By.css('.title h5')
    ).nativeElement;
    const dateElement = fixture.debugElement.query(
      By.css('.date')
    ).nativeElement;
    const timeElement = fixture.debugElement.query(
      By.css('.time')
    ).nativeElement;

    expect(sessionNameElement.textContent).toContain('Test Session');
    expect(dateElement.textContent).toContain('10/10/2023');
    expect(timeElement.textContent).toContain('10:00 AM - 12:00 PM');
  });

  it('should display "Attended" badge when session.attended is true', () => {
    const badgeElement = fixture.debugElement.query(
      By.css('.badge')
    ).nativeElement;
    expect(badgeElement.textContent).toContain('Attended');
    expect(badgeElement.classList).toContain('bg-success');
  });

  it('should display "Not Attended" badge when session.attended is false', () => {
    component.session.attended = false;
    fixture.detectChanges();
    const badgeElement = fixture.debugElement.query(
      By.css('.badge')
    ).nativeElement;
    expect(badgeElement.textContent).toContain('Not Attended');
    expect(badgeElement.classList).toContain('bg-danger');
  });

  it('should call showDetails when View Details link is clicked', () => {
    spyOn(component, 'showDetails');
    const viewDetailsLink = fixture.debugElement.query(
      By.css('#view-details-btn')
    ).nativeElement;
    viewDetailsLink.click();
    expect(component.showDetails).toHaveBeenCalled();
  });
});
