import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSessionComponent } from './edit-session-modal';
import { SessionService } from '../../services/session-service';
import { SessionModel, TeacherDetails } from '../../models/session-model';
import { of } from 'rxjs';

describe('EditSessionModal', () => {
  let component: EditSessionComponent;
  let fixture: ComponentFixture<EditSessionComponent>;
  let mockSessionService: jasmine.SpyObj<SessionService>;

  const mockSession: SessionModel = {
    sessionId: 1,
    sessionName: 'Test',
    sessionLink: 'abc',
    sessionCode: 'abc',
    date: '2026-02-02',
    startTime: '10:00',
    endTime: '11:00',
    status: 'Completed',
    teacherDetails: new TeacherDetails(),
  };

  const mockEditSessionForm = {
    sessionName: 'Test',
    date: '2026-02-02',
    startTime: '10:00',
    endTime: '11:00',
    sessionLink: 'abc',
  };

  beforeEach(async () => {
    const sessionSpy = jasmine.createSpyObj('SessionService', ['editSession']);
    await TestBed.configureTestingModule({
      imports: [EditSessionComponent],
      providers: [{ provide: SessionService, useValue: sessionSpy }],
    }).compileComponents();

    fixture = TestBed.createComponent(EditSessionComponent);
    component = fixture.componentInstance;

    mockSessionService = TestBed.inject(
      SessionService
    ) as jasmine.SpyObj<SessionService>;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should open model', () => {
    component.openModal(mockSession);

    expect(component.currentSessionId).toEqual(1);
    expect(component.editSessionForm.value).toEqual(mockEditSessionForm);
  });

  it('should update session on save', () => {
    component.editSessionForm.patchValue(mockEditSessionForm);
    component.currentSessionId = 1;
    mockSessionService.editSession.and.returnValue(of({}));

    component.onSave();

    expect(mockSessionService.editSession).toHaveBeenCalledWith(
      1,
      mockEditSessionForm as SessionModel
    );
  });
});
