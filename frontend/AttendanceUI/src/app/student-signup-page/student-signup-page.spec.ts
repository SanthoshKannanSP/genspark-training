import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentSignupPage } from './student-signup-page';

describe('StudentSignupPage', () => {
  let component: StudentSignupPage;
  let fixture: ComponentFixture<StudentSignupPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentSignupPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StudentSignupPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
