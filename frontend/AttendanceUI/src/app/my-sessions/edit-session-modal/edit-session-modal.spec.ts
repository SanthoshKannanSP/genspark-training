import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSessionComponent } from './edit-session-modal';

describe('EditSessionModal', () => {
  let component: EditSessionComponent;
  let fixture: ComponentFixture<EditSessionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditSessionComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EditSessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
