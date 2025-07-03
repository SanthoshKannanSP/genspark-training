import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveSessionBanner } from './live-session-banner';

describe('LiveSessionBanner', () => {
  let component: LiveSessionBanner;
  let fixture: ComponentFixture<LiveSessionBanner>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiveSessionBanner]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiveSessionBanner);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
