import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSessionModal } from './edit-session-modal';

describe('EditSessionModal', () => {
  let component: EditSessionModal;
  let fixture: ComponentFixture<EditSessionModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditSessionModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditSessionModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
