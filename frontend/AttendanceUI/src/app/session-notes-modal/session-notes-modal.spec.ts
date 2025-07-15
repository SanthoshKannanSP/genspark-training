import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionNotesModal } from './session-notes-modal';

describe('SessionNotesModal', () => {
  let component: SessionNotesModal;
  let fixture: ComponentFixture<SessionNotesModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SessionNotesModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SessionNotesModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
