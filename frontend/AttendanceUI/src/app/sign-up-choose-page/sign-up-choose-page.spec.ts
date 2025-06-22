import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignUpChoosePage } from './sign-up-choose-page';

describe('SignUpChoosePage', () => {
  let component: SignUpChoosePage;
  let fixture: ComponentFixture<SignUpChoosePage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignUpChoosePage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignUpChoosePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
