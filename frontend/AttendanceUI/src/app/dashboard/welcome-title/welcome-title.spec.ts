import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WelcomeTitle } from './welcome-title';

describe('WelcomeTitle', () => {
  let component: WelcomeTitle;
  let fixture: ComponentFixture<WelcomeTitle>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WelcomeTitle]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WelcomeTitle);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
