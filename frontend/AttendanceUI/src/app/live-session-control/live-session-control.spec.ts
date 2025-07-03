import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveSessionControl } from './live-session-control';

describe('LiveSessionControl', () => {
  let component: LiveSessionControl;
  let fixture: ComponentFixture<LiveSessionControl>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiveSessionControl]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiveSessionControl);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
