import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PastSessionsContainer } from './past-sessions-container';

describe('PastSessionsContainer', () => {
  let component: PastSessionsContainer;
  let fixture: ComponentFixture<PastSessionsContainer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PastSessionsContainer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PastSessionsContainer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
