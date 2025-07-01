import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherSignupPage } from './teacher-signup-page';

describe('TeacherSignupPage', () => {
  let component: TeacherSignupPage;
  let fixture: ComponentFixture<TeacherSignupPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TeacherSignupPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TeacherSignupPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
